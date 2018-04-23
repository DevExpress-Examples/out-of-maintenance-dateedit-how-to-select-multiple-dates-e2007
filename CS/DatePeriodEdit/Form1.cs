using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace DatePeriodEdit_NS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MyUsers myUsers = new MyUsers();
        bool isPopulated = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            myUsers.Add(new User("Antuan", "Acapulco", 23));
            myUsers.Add(new User("Bill", "Brucel", 17));
            myUsers.Add(new User("Charli", "Chikago", 45));
            myUsers.Add(new User("Denn", "Denver", 20));
            myUsers.Add(new User("Eva", "Everton", 23));
            gridControl1.DataSource = myUsers;

            PopulateControls();

        }

        private void PopulateControls()
        {
            multiselectComboBoxEdit1.Properties.Items.AddRange(System.Enum.GetValues(typeof(MultiselectBehaviour)));
            multiselectComboBoxEdit1.EditValue = datePeriodEdit1.Properties.OptionsSelection.MultiselectBehaviour;
            lowLevelComboBoxEdit2.Properties.Items.AddRange(System.Enum.GetValues(typeof(ViewLevel)));
            lowLevelComboBoxEdit2.EditValue = datePeriodEdit1.Properties.OptionsSelection.LowLevel;
            hightLevelComboBoxEdit3.Properties.Items.AddRange(System.Enum.GetValues(typeof(ViewLevel)));
            hightLevelComboBoxEdit3.EditValue = datePeriodEdit1.Properties.OptionsSelection.HightLevel;
            storeModeComboBoxEdit4.Properties.Items.AddRange(System.Enum.GetValues(typeof(StoreMode)));
            storeModeComboBoxEdit4.EditValue = datePeriodEdit1.Properties.PeriodsStoreMode;
            showWeekLevelCheckEdit1.Checked = datePeriodEdit1.Properties.OptionsSelection.ShowWeekLevel;
            showWeekNumbersCheckEdit.Checked = datePeriodEdit1.Properties.ShowWeekNumbers;
            separatorCharTextEdit.Text = datePeriodEdit1.Properties.SeparatorChar.ToString();
            isPopulated = true;
            AcceptControls();
        }
        private void AcceptControls()
        {
            if (!isPopulated) return;
            MultiselectBehaviour multiselectBehaviour = (MultiselectBehaviour)multiselectComboBoxEdit1.EditValue;
            ViewLevel lowLevel = (ViewLevel)lowLevelComboBoxEdit2.EditValue;
            ViewLevel highLevel = (ViewLevel)hightLevelComboBoxEdit3.EditValue;
            char separatorChar;
            bool showWeekNumbers = showWeekNumbersCheckEdit.Checked;
            bool showWeekLevel = showWeekLevelCheckEdit1.Checked;
            if (separatorCharTextEdit.Text.Length == 0)
                separatorChar = '\0';
            else                
                separatorChar = separatorCharTextEdit.Text.ToCharArray()[0]; 

            datePeriodEdit1.Properties.SeparatorChar = separatorChar;
            datePeriodEdit1.Properties.OptionsSelection.MultiselectBehaviour = multiselectBehaviour;
            datePeriodEdit1.Properties.OptionsSelection.ShowWeekLevel = showWeekLevel;
            datePeriodEdit1.Properties.OptionsSelection.LowLevel = lowLevel;
            datePeriodEdit1.Properties.OptionsSelection.HightLevel = highLevel;
            datePeriodEdit1.Properties.ShowWeekNumbers = showWeekNumbers;
            datePeriodEdit1.Properties.PeriodsStoreMode = (StoreMode)storeModeComboBoxEdit4.EditValue;

            repositoryItemDatePeriodEdit1.SeparatorChar = separatorChar;
            repositoryItemDatePeriodEdit1.OptionsSelection.MultiselectBehaviour = multiselectBehaviour;
            repositoryItemDatePeriodEdit1.OptionsSelection.ShowWeekLevel = showWeekLevel;
            repositoryItemDatePeriodEdit1.OptionsSelection.LowLevel = lowLevel;
            repositoryItemDatePeriodEdit1.OptionsSelection.HightLevel = highLevel;
            repositoryItemDatePeriodEdit1.ShowWeekNumbers = showWeekNumbers;

            repositoryItemDatePeriodEdit2.SeparatorChar = separatorChar;
            repositoryItemDatePeriodEdit2.OptionsSelection.MultiselectBehaviour = multiselectBehaviour;
            repositoryItemDatePeriodEdit2.OptionsSelection.ShowWeekLevel = showWeekLevel;
            repositoryItemDatePeriodEdit2.OptionsSelection.LowLevel = lowLevel;
            repositoryItemDatePeriodEdit2.OptionsSelection.HightLevel = highLevel;
            repositoryItemDatePeriodEdit2.ShowWeekNumbers = showWeekNumbers;
        }


        private void datePeriodEdit1_EditValueChanged(object sender, EventArgs e)
        {
            this.Text = datePeriodEdit1.EditValue.GetType().ToString() + ": " + datePeriodEdit1.EditValue.ToString();
        }

        private void EditValueChanged(object sender, EventArgs e)
        {
            AcceptControls();
        }

        private void lowLevelComboBoxEdit2_EditValueChanged(object sender, EventArgs e)
        {
            if (hightLevelComboBoxEdit3.SelectedIndex < lowLevelComboBoxEdit2.SelectedIndex)
                hightLevelComboBoxEdit3.SelectedIndex = lowLevelComboBoxEdit2.SelectedIndex;
            AcceptControls();
        }

        private void hightLevelComboBoxEdit3_EditValueChanged(object sender, EventArgs e)
        {
            if (hightLevelComboBoxEdit3.SelectedIndex < lowLevelComboBoxEdit2.SelectedIndex)
                lowLevelComboBoxEdit2.SelectedIndex = hightLevelComboBoxEdit3.SelectedIndex;
            AcceptControls();
        }

       
    }
    public class User
    {
        string name, city, periodsString;
        int age;
        PeriodsSet periodsSet;
        public User(string name, string city, int age) {
            this.name = name;
            this.city = city;
            this.age = age;
            periodsSet = new PeriodsSet();
            periodsSet.MergeWith(DateTime.Today, DateTime.Today);
            periodsSet.MergeWith(DateTime.Today.AddDays(5), DateTime.Today.AddDays(8));
            periodsString = periodsSet.ToString();
        }
        public int Age { set { age = value; } get { return age; } }
        public string Name { set { name = value; } get { return name; } }
        public string City { set { city = value; } get { return city; } }
        public string PeriodsString { 
            set { periodsString = value; } 
            get { return periodsString; } }
        public PeriodsSet PeriodsSet
        { 
            set { periodsSet = value; } 
            get { return periodsSet; } }
    }
    public class MyUsers : ArrayList {
        public new virtual User this[int index]{ get { return base[index] as User; } set { base[index] = value; } }
    }
    
}