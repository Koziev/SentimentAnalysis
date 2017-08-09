namespace ScoringCorpusEditor
{
    partial class ScoringCorpusEditorMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lv_lines = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cb_minus = new System.Windows.Forms.Button();
            this.cb_0 = new System.Windows.Forms.Button();
            this.cb_plus = new System.Windows.Forms.Button();
            this.cb_load_corpus = new System.Windows.Forms.Button();
            this.tb_path = new System.Windows.Forms.TextBox();
            this.cb_select_path = new System.Windows.Forms.Button();
            this.tb_curent = new System.Windows.Forms.TextBox();
            this.cb_save_corpus = new System.Windows.Forms.Button();
            this.tb_labels = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbx_aspects = new System.Windows.Forms.ComboBox();
            this.cb_aspect_0 = new System.Windows.Forms.Button();
            this.cb_aspect_1 = new System.Windows.Forms.Button();
            this.cbx_modifiers = new System.Windows.Forms.ComboBox();
            this.cb_modifier_0 = new System.Windows.Forms.Button();
            this.cb_modifier_1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb_modifier = new System.Windows.Forms.RadioButton();
            this.rb_aspect = new System.Windows.Forms.RadioButton();
            this.rb_polarity = new System.Windows.Forms.RadioButton();
            this.cb_plus_minus = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tb_id = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lv_lines
            // 
            this.lv_lines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv_lines.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader1,
            this.columnHeader2});
            this.lv_lines.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lv_lines.FullRowSelect = true;
            this.lv_lines.GridLines = true;
            this.lv_lines.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lv_lines.HideSelection = false;
            this.lv_lines.Location = new System.Drawing.Point(13, 47);
            this.lv_lines.MultiSelect = false;
            this.lv_lines.Name = "lv_lines";
            this.lv_lines.ShowGroups = false;
            this.lv_lines.Size = new System.Drawing.Size(1672, 599);
            this.lv_lines.TabIndex = 0;
            this.lv_lines.UseCompatibleStateImageBehavior = false;
            this.lv_lines.View = System.Windows.Forms.View.Details;
            this.lv_lines.VirtualMode = true;
            this.lv_lines.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.lv_lines_RetrieveVirtualItem);
            this.lv_lines.SelectedIndexChanged += new System.EventHandler(this.lv_lines_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "№";
            this.columnHeader3.Width = 164;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "text";
            this.columnHeader1.Width = 1208;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "sign";
            this.columnHeader2.Width = 285;
            // 
            // cb_minus
            // 
            this.cb_minus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_minus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cb_minus.Location = new System.Drawing.Point(1446, 661);
            this.cb_minus.Name = "cb_minus";
            this.cb_minus.Size = new System.Drawing.Size(75, 23);
            this.cb_minus.TabIndex = 1;
            this.cb_minus.Text = "-";
            this.cb_minus.UseVisualStyleBackColor = false;
            this.cb_minus.Click += new System.EventHandler(this.cb_minus_Click);
            // 
            // cb_0
            // 
            this.cb_0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_0.BackColor = System.Drawing.Color.White;
            this.cb_0.Location = new System.Drawing.Point(1527, 661);
            this.cb_0.Name = "cb_0";
            this.cb_0.Size = new System.Drawing.Size(75, 23);
            this.cb_0.TabIndex = 2;
            this.cb_0.Text = "0";
            this.cb_0.UseVisualStyleBackColor = false;
            this.cb_0.Click += new System.EventHandler(this.cb_0_Click);
            // 
            // cb_plus
            // 
            this.cb_plus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_plus.BackColor = System.Drawing.Color.Lime;
            this.cb_plus.Location = new System.Drawing.Point(1610, 660);
            this.cb_plus.Name = "cb_plus";
            this.cb_plus.Size = new System.Drawing.Size(75, 23);
            this.cb_plus.TabIndex = 3;
            this.cb_plus.Text = "+";
            this.cb_plus.UseVisualStyleBackColor = false;
            this.cb_plus.Click += new System.EventHandler(this.cb_plus_Click);
            // 
            // cb_load_corpus
            // 
            this.cb_load_corpus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_load_corpus.Location = new System.Drawing.Point(1529, 11);
            this.cb_load_corpus.Name = "cb_load_corpus";
            this.cb_load_corpus.Size = new System.Drawing.Size(75, 23);
            this.cb_load_corpus.TabIndex = 4;
            this.cb_load_corpus.Text = "Load";
            this.cb_load_corpus.UseVisualStyleBackColor = true;
            this.cb_load_corpus.Click += new System.EventHandler(this.cb_load_corpus_Click);
            // 
            // tb_path
            // 
            this.tb_path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_path.Location = new System.Drawing.Point(671, 12);
            this.tb_path.Name = "tb_path";
            this.tb_path.Size = new System.Drawing.Size(852, 22);
            this.tb_path.TabIndex = 5;
            // 
            // cb_select_path
            // 
            this.cb_select_path.Location = new System.Drawing.Point(590, 11);
            this.cb_select_path.Name = "cb_select_path";
            this.cb_select_path.Size = new System.Drawing.Size(75, 23);
            this.cb_select_path.TabIndex = 6;
            this.cb_select_path.Text = "Select";
            this.cb_select_path.UseVisualStyleBackColor = true;
            this.cb_select_path.Click += new System.EventHandler(this.cb_select_path_Click);
            // 
            // tb_curent
            // 
            this.tb_curent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tb_curent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tb_curent.Location = new System.Drawing.Point(91, 661);
            this.tb_curent.Name = "tb_curent";
            this.tb_curent.Size = new System.Drawing.Size(860, 30);
            this.tb_curent.TabIndex = 7;
            // 
            // cb_save_corpus
            // 
            this.cb_save_corpus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_save_corpus.Enabled = false;
            this.cb_save_corpus.Location = new System.Drawing.Point(1610, 11);
            this.cb_save_corpus.Name = "cb_save_corpus";
            this.cb_save_corpus.Size = new System.Drawing.Size(75, 23);
            this.cb_save_corpus.TabIndex = 8;
            this.cb_save_corpus.Text = "Save";
            this.cb_save_corpus.UseVisualStyleBackColor = true;
            this.cb_save_corpus.Click += new System.EventHandler(this.cb_save_corpus_Click);
            // 
            // tb_labels
            // 
            this.tb_labels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tb_labels.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tb_labels.Location = new System.Drawing.Point(91, 697);
            this.tb_labels.Name = "tb_labels";
            this.tb_labels.Size = new System.Drawing.Size(860, 30);
            this.tb_labels.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 671);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Sentence:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 707);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Labels:";
            // 
            // cbx_aspects
            // 
            this.cbx_aspects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbx_aspects.FormattingEnabled = true;
            this.cbx_aspects.Items.AddRange(new object[] {
            "бефстроганов",
            "абрикосовый_ликер",
            "абрикосы",
            "авокадо",
            "администрация",
            "аквапарк",
            "акула",
            "алкоголь",
            "американо",
            "амфитеатр",
            "ананас",
            "ангиология",
            "анимация",
            "апельсиновый_фреш",
            "апельсины",
            "арбуз",
            "аэробика",
            "аюрведа",
            "бадминтон",
            "балкон",
            "бананы",
            "банкомат",
            "баня",
            "бар",
            "бар_у_бассейна",
            "баранина",
            "барбекю",
            "барракуда",
            "баскетбол",
            "бассейн",
            "батут",
            "бельгийские_вафли",
            "библиотека",
            "биде",
            "бизнес_отель",
            "бизнес_центр",
            "бильярд",
            "биосок",
            "бисквиты",
            "блины",
            "бочча",
            "бренди",
            "буженина",
            "бутерброды",
            "бюджетный_отель",
            "ванна",
            "ванная",
            "ванные_принадлежности",
            "вареные_сосиски",
            "вареные_яйца",
            "варенье",
            "вафли",
            "велнес",
            "велосипеды",
            "венский_шницель",
            "вентиляция",
            "вермишелевый_суп",
            "вермут",
            "ветчина",
            "вид_на_город",
            "вид_на_горы",
            "вид_на_лес",
            "вид_на_море",
            "вид_на_набережную",
            "вид_на_озеро",
            "вид_на_реку",
            "виндсерфинг",
            "вино",
            "виноград",
            "виски",
            "вишневый_ликер",
            "водка",
            "водное_поло",
            "водные_горки",
            "водные_развлечения",
            "водоросли",
            "волейбол",
            "восточная_кухня",
            "вотербол",
            "выпечка",
            "вытяжение",
            "гавайская_смесь",
            "галечный_пляж",
            "гарниры",
            "гастроэнтеролог",
            "гель_для_душа",
            "гибы",
            "гинеколог",
            "глазунья",
            "глинтвейн",
            "говядина",
            "говяжий_язык",
            "голубцы",
            "гольф",
            "горнолыжный_отель",
            "горные_велосипеды",
            "городской_пляж",
            "гороховый_суп",
            "горячий_шоколад",
            "гостиная",
            "грейпфрут",
            "гренки",
            "греческие_танцы",
            "греческий_салат",
            "гречка",
            "гречневая_каша",
            "грибной_соус",
            "гриль",
            "грушевый_сок",
            "груши",
            "грязелечение",
            "дайвинг",
            "двухъярусная_кровать",
            "дерматолог",
            "дерматология",
            "десерты",
            "детская_анимация",
            "детская_комната",
            "детская_кроватка",
            "детская_площадка",
            "детская_программа",
            "детские_горки",
            "детский_бассейн",
            "детский_клуб",
            "детский_стол",
            "джакузи",
            "джем",
            "джин",
            "диетическое_питание",
            "дискотека",
            "доктор",
            "дорада",
            "дуриан",
            "духовка",
            "душ_шарко",
            "душевая_кабина",
            "дыня",
            "жалюзи",
            "жареные_сосиски",
            "жареный_перец",
            "жаркое",
            "жемчужные_ванны",
            "живая_музыка",
            "завтрак",
            "запеченная_семга",
            "звукоизоляция",
            "игровая_комната",
            "изюм",
            "индейка",
            "интернет",
            "инфракрасная_сауна",
            "исторический_отель",
            "итальянский_ресторан",
            "йога",
            "йогурт",
            "кабельное_тв",
            "казино",
            "какао",
            "калифорнийские_ванны",
            "кальмар",
            "камбала",
            "камера_хранения",
            "каноэ",
            "капуста",
            "капуччино",
            "караоке",
            "карстовая_пещера",
            "картофель",
            "картофель_жареный",
            "картофель_тушеный",
            "картофель_фри",
            "картофельное_пюре",
            "картофельные_драники",
            "каши",
            "квас",
            "кексы",
            "кефир",
            "киви",
            "кисломолочные_продукты",
            "клубника",
            "клубника_в_шоколаде",
            "ковер",
            "коктейль",
            "кола",
            "колбаса",
            "кондитерские_изделия",
            "кондиционер",
            "консервированные_ананасы",
            "консьерж",
            "конференц_зал",
            "коньяк",
            "косметический_салон",
            "котлеты",
            "кофе",
            "кофемашина",
            "креветки",
            "кровать",
            "кролик",
            "купели",
            "куриная_печень",
            "курица",
            "кухонный_блок",
            "лагуна",
            "лангусты",
            "латте",
            "лежаки",
            "лечение",
            "лечо",
            "ликер",
            "лимонад",
            "лифт",
            "макароны",
            "макрель",
            "манго",
            "манговое_пюре",
            "мартини",
            "масло",
            "массаж",
            "матрас",
            "мебель",
            "мёд",
            "медицинский_кабинет",
            "медперсонал",
            "механа",
            "мидии",
            "микроволновая_печь",
            "минералка",
            "минибар",
            "молоко",
            "море",
            "морепродукты",
            "морковь",
            "мороженое",
            "морской_еж",
            "мыло",
            "мюсли",
            "мясная_нарезка",
            "мясо",
            "мясо_запеченное",
            "напитки",
            "нарезка",
            "настольный_футбол",
            "невролог",
            "неврология",
            "нектарины",
            "номер",
            "нордическая_ходьба",
            "ночной_клуб",
            "няня",
            "обед",
            "обмен_валют",
            "общая_тональность",
            "общее_впечатление",
            "овощи",
            "овощной_салат",
            "овсяная_каша",
            "огурцы",
            "озонотерапия",
            "окунь",
            "оливки",
            "омлет",
            "орехи",
            "осьминоги",
            "отварной_картофель",
            "отель",
            "отельный_комплекс",
            "отопление",
            "официанты",
            "охрана",
            "палтус",
            "пандусы",
            "парковка",
            "паштет",
            "пейзаж",
            "пейнтбол",
            "персики",
            "персонал",
            "песчаный_пляж",
            "печенье",
            "пиво",
            "пирог",
            "письменный_стол",
            "питание",
            "пицца",
            "плов",
            "пляж",
            "пляжный_отель",
            "подъемники",
            "полотенца",
            "полуторная_кровать",
            "пончики",
            "постель_(белье)",
            "посуда",
            "прачечная",
            "природа",
            "прокат_велосипедов",
            "прокат_снаряжения",
            "простыни",
            "птица",
            "рагу",
            "рагу_из_косули",
            "радио",
            "развлечения",
            "развлечения_для_детей",
            "размещение",
            "раки",
            "ракия",
            "рапаны",
            "расположение",
            "рафтинг",
            "ресторан",
            "речная_рыба",
            "ризотто",
            "римская_баня",
            "рис",
            "рисовое_молоко",
            "российское",
            "русская_баня",
            "рыба",
            "рыба_в_тесте",
            "рыбное_филе",
            "салат",
            "самбука",
            "санаторий",
            "сантехника",
            "свинина",
            "сдоба",
            "сейф",
            "семга",
            "семейный_отель",
            "сервис",
            "серфинг",
            "сметана",
            "собственный_пляж",
            "соки",
            "солевые_ингаляции",
            "солянка",
            "солярий",
            "сосиски",
            "соусы",
            "спа",
            "спагетти",
            "специи",
            "спорт",
            "спортзал",
            "спрайт",
            "стадион",
            "стейк",
            "стиральная_машина",
            "стоматолог",
            "стоматология",
            "стрельба",
            "стрельба_из_винтовки",
            "суп",
            "суп_куриный",
            "суп_пюре",
            "суфле",
            "сухой_завтрак",
            "сухофрукты",
            "сушеные_банананы",
            "сыр",
            "тайская_кухня",
            "тарзанка",
            "творог",
            "текила",
            "телевидение",
            "телевизор",
            "телятина",
            "телячьи_стейки",
            "теннисный_корт",
            "термальная_вода",
            "термальный_бассейн",
            "термальный_комплекс",
            "территория",
            "тест_5",
            "тефтели",
            "тир",
            "тоник",
            "торты",
            "тостер",
            "тосты",
            "травяная_баня",
            "травяной_чай",
            "трансфер",
            "тропический_душ",
            "туалетная_бумага",
            "тумбочки",
            "тунец",
            "турецкая_баня",
            "тушеное_мясо",
            "тушеные_овощи",
            "уборка",
            "ужин",
            "утка",
            "утюг",
            "фанта",
            "фаршированный_перец",
            "фасолевый_суп",
            "фасоль",
            "фен",
            "физиотерапия",
            "филиппинское_манго",
            "финики",
            "финская_баня",
            "фотосъемка",
            "фруктовое_ассорти",
            "фруктовый_салат",
            "фрукты",
            "футбол",
            "халаты",
            "химчистка",
            "хлеб",
            "хлопья",
            "хозяева",
            "холодильник",
            "холодная_нарезка",
            "чай",
            "чайник",
            "чайный_набор",
            "черешня",
            "черника",
            "чернослив",
            "шампанское",
            "шампиньоны",
            "шампиньоны_тушеные",
            "шампунь",
            "шварцвальдский_торт",
            "швепс",
            "шкаф",
            "школа_танцев",
            "шоколадные_шарики",
            "шоколадный_ликер",
            "экскурсия",
            "электронный_ключ",
            "эндокринолог",
            "эндокринология",
            "эспрессо",
            "яблочный_сок",
            "ягненок",
            "ягоды",
            "яичница",
            "яйца",
            "японская_кухня"});
            this.cbx_aspects.Location = new System.Drawing.Point(91, 741);
            this.cbx_aspects.Name = "cbx_aspects";
            this.cbx_aspects.Size = new System.Drawing.Size(249, 24);
            this.cbx_aspects.TabIndex = 12;
            // 
            // cb_aspect_0
            // 
            this.cb_aspect_0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_aspect_0.Location = new System.Drawing.Point(346, 741);
            this.cb_aspect_0.Name = "cb_aspect_0";
            this.cb_aspect_0.Size = new System.Drawing.Size(75, 23);
            this.cb_aspect_0.TabIndex = 13;
            this.cb_aspect_0.Text = "set to 0";
            this.cb_aspect_0.UseVisualStyleBackColor = true;
            this.cb_aspect_0.Click += new System.EventHandler(this.cb_aspect_0_Click);
            // 
            // cb_aspect_1
            // 
            this.cb_aspect_1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_aspect_1.Location = new System.Drawing.Point(427, 742);
            this.cb_aspect_1.Name = "cb_aspect_1";
            this.cb_aspect_1.Size = new System.Drawing.Size(75, 23);
            this.cb_aspect_1.TabIndex = 14;
            this.cb_aspect_1.Text = "set to 1";
            this.cb_aspect_1.UseVisualStyleBackColor = true;
            this.cb_aspect_1.Click += new System.EventHandler(this.cb_aspect_1_Click);
            // 
            // cbx_modifiers
            // 
            this.cbx_modifiers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbx_modifiers.FormattingEnabled = true;
            this.cbx_modifiers.Items.AddRange(new object[] {
            "ассортимент",
            "качество",
            "количество",
            "вкусность",
            "дешевизна",
            "близость"});
            this.cbx_modifiers.Location = new System.Drawing.Point(579, 739);
            this.cbx_modifiers.Name = "cbx_modifiers";
            this.cbx_modifiers.Size = new System.Drawing.Size(217, 24);
            this.cbx_modifiers.TabIndex = 15;
            // 
            // cb_modifier_0
            // 
            this.cb_modifier_0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_modifier_0.Location = new System.Drawing.Point(802, 740);
            this.cb_modifier_0.Name = "cb_modifier_0";
            this.cb_modifier_0.Size = new System.Drawing.Size(75, 23);
            this.cb_modifier_0.TabIndex = 16;
            this.cb_modifier_0.Text = "set to 0";
            this.cb_modifier_0.UseVisualStyleBackColor = true;
            this.cb_modifier_0.Click += new System.EventHandler(this.cb_modifier_0_Click);
            // 
            // cb_modifier_1
            // 
            this.cb_modifier_1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_modifier_1.Location = new System.Drawing.Point(883, 741);
            this.cb_modifier_1.Name = "cb_modifier_1";
            this.cb_modifier_1.Size = new System.Drawing.Size(75, 23);
            this.cb_modifier_1.TabIndex = 17;
            this.cb_modifier_1.Text = "set to 1";
            this.cb_modifier_1.UseVisualStyleBackColor = true;
            this.cb_modifier_1.Click += new System.EventHandler(this.cb_modifier_1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.rb_modifier);
            this.groupBox1.Controls.Add(this.rb_aspect);
            this.groupBox1.Controls.Add(this.rb_polarity);
            this.groupBox1.Location = new System.Drawing.Point(1020, 665);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 107);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Highlighting";
            // 
            // rb_modifier
            // 
            this.rb_modifier.AutoSize = true;
            this.rb_modifier.Location = new System.Drawing.Point(6, 80);
            this.rb_modifier.Name = "rb_modifier";
            this.rb_modifier.Size = new System.Drawing.Size(79, 21);
            this.rb_modifier.TabIndex = 2;
            this.rb_modifier.Text = "Modifier";
            this.rb_modifier.UseVisualStyleBackColor = true;
            // 
            // rb_aspect
            // 
            this.rb_aspect.AutoSize = true;
            this.rb_aspect.Location = new System.Drawing.Point(7, 50);
            this.rb_aspect.Name = "rb_aspect";
            this.rb_aspect.Size = new System.Drawing.Size(72, 21);
            this.rb_aspect.TabIndex = 1;
            this.rb_aspect.Text = "Aspect";
            this.rb_aspect.UseVisualStyleBackColor = true;
            // 
            // rb_polarity
            // 
            this.rb_polarity.AutoSize = true;
            this.rb_polarity.Checked = true;
            this.rb_polarity.Location = new System.Drawing.Point(7, 22);
            this.rb_polarity.Name = "rb_polarity";
            this.rb_polarity.Size = new System.Drawing.Size(76, 21);
            this.rb_polarity.TabIndex = 0;
            this.rb_polarity.TabStop = true;
            this.rb_polarity.Text = "Polarity";
            this.rb_polarity.UseVisualStyleBackColor = true;
            // 
            // cb_plus_minus
            // 
            this.cb_plus_minus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_plus_minus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.cb_plus_minus.Location = new System.Drawing.Point(1527, 707);
            this.cb_plus_minus.Name = "cb_plus_minus";
            this.cb_plus_minus.Size = new System.Drawing.Size(75, 23);
            this.cb_plus_minus.TabIndex = 19;
            this.cb_plus_minus.Text = "- & +";
            this.cb_plus_minus.UseVisualStyleBackColor = false;
            this.cb_plus_minus.Click += new System.EventHandler(this.cb_plus_minus_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(119, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "go to";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tb_id
            // 
            this.tb_id.Location = new System.Drawing.Point(13, 11);
            this.tb_id.Name = "tb_id";
            this.tb_id.Size = new System.Drawing.Size(100, 22);
            this.tb_id.TabIndex = 21;
            // 
            // ScoringCorpusEditorMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1697, 784);
            this.Controls.Add(this.tb_id);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cb_plus_minus);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cb_modifier_1);
            this.Controls.Add(this.cb_modifier_0);
            this.Controls.Add(this.cbx_modifiers);
            this.Controls.Add(this.cb_aspect_1);
            this.Controls.Add(this.cb_aspect_0);
            this.Controls.Add(this.cbx_aspects);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_labels);
            this.Controls.Add(this.cb_save_corpus);
            this.Controls.Add(this.tb_curent);
            this.Controls.Add(this.cb_select_path);
            this.Controls.Add(this.tb_path);
            this.Controls.Add(this.cb_load_corpus);
            this.Controls.Add(this.cb_plus);
            this.Controls.Add(this.cb_0);
            this.Controls.Add(this.cb_minus);
            this.Controls.Add(this.lv_lines);
            this.Name = "ScoringCorpusEditorMainForm";
            this.Text = "Scoring Corpus Editor";
            this.Load += new System.EventHandler(this.ScoringCorpusEditorMainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lv_lines;
        private System.Windows.Forms.Button cb_minus;
        private System.Windows.Forms.Button cb_0;
        private System.Windows.Forms.Button cb_plus;
        private System.Windows.Forms.Button cb_load_corpus;
        private System.Windows.Forms.TextBox tb_path;
        private System.Windows.Forms.Button cb_select_path;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.TextBox tb_curent;
        private System.Windows.Forms.Button cb_save_corpus;
        private System.Windows.Forms.TextBox tb_labels;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbx_aspects;
        private System.Windows.Forms.Button cb_aspect_0;
        private System.Windows.Forms.Button cb_aspect_1;
        private System.Windows.Forms.ComboBox cbx_modifiers;
        private System.Windows.Forms.Button cb_modifier_0;
        private System.Windows.Forms.Button cb_modifier_1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rb_modifier;
        private System.Windows.Forms.RadioButton rb_aspect;
        private System.Windows.Forms.RadioButton rb_polarity;
        private System.Windows.Forms.Button cb_plus_minus;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tb_id;
    }
}

