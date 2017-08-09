# -*- coding: utf-8 -*-
'''
Модель для определения полярности предложений (-1,0,+1)
https://github.com/fchollet/keras/blob/master/examples/imdb_cnn.py

Используется выгрузка корпуса из Редактора Оценок с метками negat=1,
nosign=1 и posit=1
'''

from __future__ import print_function
import numpy as np

from keras.preprocessing import sequence
from keras.models import Sequential
from keras.layers import Dense, Dropout, Activation
from keras.callbacks import ModelCheckpoint, EarlyStopping
from keras.layers import Embedding
from keras.optimizers import SGD, RMSprop, Adamax, Adagrad
from keras.layers import Convolution1D, GlobalMaxPooling1D
from collections import Counter
import codecs
import re
import json

TRAINING_SIZE = 150000
MAX_SENT_LEN = 20
LEMMATIZE = False
MODEL_WEIGHTS_FILENAME = 'polarity_cnn.model'
ARCH_FILENAME = 'polarity_cnn.arch'
DATASET_CONFIG = 'dataset.config'


max_nb_words = 100000
maxlen = MAX_SENT_LEN #400
batch_size = 32
embedding_dims = 256
nb_filter = 250
filter_length = 2 #было 3
hidden_dims = 256
nb_epoch = 100


# ----------------------------------------------------------------------

def normalize_word( w ):
    if w[0].isdigit():
        return u'_num_';
    else:
        return w.lower().replace( u'ё', u'е' )

regex = re.compile(r'[%s\s]+' % re.escape( u'[; .,?!-…№”“\'"–—_:«»*]()' ))
def split2words( phrase ):
    return [ normalize_word(w) for w in regex.split(phrase) if len(w)>0 ]

#cyr_chars = { c for c in u'абвгдежзийклмнопрстуфхцчшщъыьэюя' }
cyr_chars = { c for c in u'абвгдежзийклмнопрстуфхцчшщъыьэюя0123456789+' }
def all_chars_cyr( word ):
    for c in word:
        if not c in cyr_chars:
            return False
    return True

word2id = dict()
def get_word_id( word ):
    if word in word2id:
        return word2id[word]
    else:
      wid = len(word2id)
      word2id[word] = wid
      return wid


word2lemma = dict()
def lemmatize( word ):
    if word in word2lemma:
        return word2lemma[word]
    else:
        return word;

# ----------------------------------------------------------------------

if LEMMATIZE:
    # Загружаем лемматизатор
    print( 'Loading lemmas...' )
    acceptable_classes = { u'СУЩЕСТВИТЕЛЬНОЕ', u'ПРИЛАГАТЕЛЬНОЕ', u'ГЛАГОЛ', u'НАРЕЧИЕ' }
    with codecs.open( 'word2lemma.dat', 'r', 'utf-8' ) as rdr:
        for line0 in rdr:
            line = line0.strip()
            if len(line)>0:
                tx = line.split(u'\t')
                if len(tx)==3 and tx[2] in acceptable_classes:
                    word = tx[0].lower()
                    lemma = tx[1].lower()
                    word2lemma[word] = lemma
                    
    print( 'Done, {0} wordforms'.format( len(word2lemma) ) )

# ----------------------------------------------------------------------


print('Loading data...')

data_files = ['polarity_corpus.dat']
max_sent_len = 0

all_words = Counter()
for srcpath in data_files:
    line_count=0
    print( u'Loading dataset {0}...'.format(srcpath) )
    with codecs.open(srcpath, "r", "utf-8") as fdata:
        for line in fdata:
            #if line_count>=TRAINING_SIZE: break
            if line=='': break
    
            line = line.strip()
            line_count += 1
            toks = line.split('\t')
            if len(toks)==2:
                sent = toks[0]
                for word in split2words(sent):
                    #if all_chars_cyr(word):
                        all_words[ lemmatize(word) ] += 1


# оставим только TOP_WORDS самых частотных слов
max_nb_words = min( max_nb_words, len(all_words) )
TOP_WORDS = max_nb_words
acceptable_words = set( w for (w,c) in sorted( [ (w,c) for (w,c) in all_words.iteritems() ], key=lambda z: -z[1] )[:TOP_WORDS] )

src_sentences = []
X_data = []
y_data = []
for srcpath in data_files:
    line_count=0
    print( u'Loading dataset {0}...'.format(srcpath) )
    with codecs.open(srcpath, "r", "utf-8") as fdata:
        for line in fdata:
            if line_count>=TRAINING_SIZE: break
            if line=='': break
    
            line = line.strip()
            toks = line.split('\t')
            if len(toks)==2:
                sent = toks[0]
                labels = toks[1]

                if labels.find('negat=1')!=-1 and labels.find('posit=1')!=-1:
                    continue # пропускаем разнополярные оценки
                
                sign = np.zeros( (3), dtype=np.bool )
                if labels.find('negat=1')!=-1:
                    sign[0]=True
                elif labels.find('posit=1')!=-1:
                    sign[2]=True
                else:
                    sign[1]=True
                    
                #print( u'DEBUG sent={0} labels={1} sign={2}'.format( sent, labels, sign ) )
                #raw_input()

                words = [ get_word_id( lemmatize(w) ) for w in split2words(sent) if w in acceptable_words ]
                if len(words)<=MAX_SENT_LEN:
                    max_sent_len = max( max_sent_len, len(words) )
                    X_data.append( words )
                    y_data.append( sign )
                    src_sentences.append(sent)
                    line_count += 1

maxlen = max_sent_len
n_patterns = len(X_data)
print( 'max_word_id=', len(word2id) )
print( 'Total number of patterns={0}, max_sent_len={1}'.format(n_patterns,max_sent_len) )


with open(DATASET_CONFIG,'w') as cfg:
    params = { 
              'max_sent_len':max_sent_len,
              'LEMMATIZE':LEMMATIZE,
              'cyr_chars':[ c for c in cyr_chars]
             }
    json.dump( params, cfg )

# сохраним список слов для последующего использования при оценке корпуса отзывов
with codecs.open( 'word2id.dat', 'w', 'utf-8' ) as wrt:
    for word,id in word2id.iteritems():
        wrt.write( u'{0}\t{1}\n'.format(word,id) )


# ----------------------------------------------------------------------


n_test = int(n_patterns*0.1)
n_train = n_patterns-n_test
print( 'n_test={0} n_train={1}'.format(n_test,n_train) )
data_indeces = [ x for x in range(n_patterns) ]
np.random.shuffle( data_indeces )
test_indeces = data_indeces[ : n_test ]
train_indeces = data_indeces[ n_test : ]

X_train = []
y_train = np.zeros( (n_train,3), dtype=np.bool )
for i in range(n_train):
    X_train.append( X_data[ train_indeces[i] ] )
    y_train[i] = y_data[ train_indeces[i] ]

X_train = sequence.pad_sequences( X_train, maxlen=maxlen)

X_test = []
y_test = np.zeros( (n_test,3), dtype=np.bool )
for i in range(n_test):
    X_test.append( X_data[ test_indeces[i] ] )
    y_test[i] = y_data[ test_indeces[i] ]

X_test = sequence.pad_sequences( X_test, maxlen=maxlen)
    

print(len(X_train), 'train sequences')
print(len(X_test), 'test sequences')

# ----------------------------------------------------------------------

print('Build model...')
model = Sequential()

model.add(Embedding(max_nb_words,
                    embedding_dims,
                    input_length=maxlen,
                    dropout=0.2))

model.add(Convolution1D(nb_filter=nb_filter,
                        filter_length=filter_length,
                        border_mode='valid',
                        activation='relu',
                        subsample_length=1))
model.add(GlobalMaxPooling1D())

model.add(Dense(hidden_dims))
model.add(Dropout(0.2))
model.add(Activation('relu'))

model.add(Dense(3))
model.add(Activation('softmax'))


opt = Adamax(lr=0.01, beta_1=0.9, beta_2=0.999, epsilon=1e-08)
#opt = Adagrad(lr=0.05, epsilon=1e-08)
#opt='rmsprop'
#opt='adam'

model.compile(loss='categorical_crossentropy', optimizer=opt, metrics=['accuracy'])


open( ARCH_FILENAME, 'w').write( model.to_json() )




model_checkpoint = ModelCheckpoint( MODEL_WEIGHTS_FILENAME, monitor='val_acc', verbose=1, save_best_only=True, mode='auto')
early_stopping = EarlyStopping( monitor='val_acc', patience=10, verbose=1, mode='auto')

model.fit(X_train, y_train,
          batch_size=batch_size,
          nb_epoch=nb_epoch,
          validation_data=(X_test, y_test), callbacks=[model_checkpoint,early_stopping] )


# сгенерируем HTML файл с оценками предложений в тестовом наборе
model.load_weights( MODEL_WEIGHTS_FILENAME )
z_test = model.predict_classes( X_test )

INLINE_STYLES = True

with codecs.open( 'polarity_test.html', 'w', 'utf-8' ) as wrt:
    
    wrt.write( '<html>\n' )
    wrt.write( '<head>\n' )
    wrt.write( '<meta charset="utf-8">\n' )
    wrt.write( '<title>Scoring of test dataset</title>\n' )
    wrt.write( '</head>\n' )
    wrt.write( '<body>\n' )
    
    if INLINE_STYLES==False:
        wrt.write( '<style>\n' )
        wrt.write( '.neg { background-color:#FF8080; color:black }\n' )
        wrt.write( '.z { background-color:#FFFFFF; color:black }\n' )
        wrt.write( '.pos { background-color:#80ff80; color:black }\n' )
        wrt.write( '</style>\n' )
    
    for sign in range(3):
        
        if INLINE_STYLES==True:
            if sign==0:
                wrt.write( "<span style='background-color:#FF8080'>" )
            elif sign==1:
                wrt.write( "<span style='background-color:#FFFFFF'>" )
            elif sign==2:
                wrt.write( "<span style='background-color:#80ff80'>" )
        else:
            if sign==0:
                wrt.write( "<span class='neg'>" )
            elif sign==1:
                wrt.write( "<span class='z'>" )
            elif sign==2:
                wrt.write( "<span class='pos'>" )
        
        for i in range(len(X_test)):
            if sign==z_test[i]:
                wrt.write( u'{0}<br/>\n'.format(src_sentences[test_indeces[i]]) )

        wrt.write( '</span>\n' )

    wrt.write( '</body>\n' )
    wrt.write( '</html>\n' )
  

print( '\n\n' )
while True:
    sent = raw_input( 'Enter phrase: ' ).decode( 'utf-8' ).strip()
    if len(sent)>0:
        words = [ get_word_id( lemmatize(w) ) for w in split2words(sent) if w in acceptable_words ]
        X_query = []
        X_query.append( words )
        X_query = sequence.pad_sequences( X_query, maxlen=maxlen)
        y_query = model.predict_classes( X_query, verbose=False )
        sign = y_query[0]
        if sign==0:
            print( '-1' )
        elif sign==1:
            print( '0' )
        elif sign==2:
            print( '+1' )

