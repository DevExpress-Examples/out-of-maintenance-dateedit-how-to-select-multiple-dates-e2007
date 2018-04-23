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
        private void Form1_Load(object sender, EventArgs e)
        {
            myUsers.Add(new User("Antuan", "Acapulco", 23));
            myUsers.Add(new User("Bill", "Brucel", 17));
            myUsers.Add(new User("Charli", "Chikago", 45));
            myUsers.Add(new User("Denn", "Denver", 20));
            myUsers.Add(new User("Eva", "Everton", 23));
            gridControl1.DataSource = myUsers;
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
           // periodsSet.MergeWith(DateTime.Today.AddDays(5), DateTime.Today.AddDays(8));
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