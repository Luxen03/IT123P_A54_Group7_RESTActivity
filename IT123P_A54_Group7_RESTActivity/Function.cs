using Android.App;
using Android.Content;
using Android.Icu.Text;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Nio.FileNio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Provider.UserDictionary;
using static Java.Util.Jar.Attributes;

namespace MyRESTfulDB
{
    internal class Function
    {
        private Transact transact;
        private string ipAdd = "192.168.1.2", res;
        private readonly Activity activity;

        public Function(Activity activity)
        {this.activity = activity;}

        public void NextAct(string uname) 
        {
            if (res.Contains("OK!"))
            {
                Intent i = new Intent(activity, typeof(NextActivity));
                i.PutExtra("Name", uname);
                activity.StartActivity(i);
            }
        }

        public void Login(string uname, string pword)
        {
            transact = new Transact(ipAdd, uname, pword);
            res = transact.LogRequest();
            Toast.MakeText(activity, res, ToastLength.Long).Show();
            NextAct(uname);
        }

        public void AddRecord(string name, string school, string country, string selectedGender, EditText editsID)
        {
            transact = new Transact(ipAdd, name, school, country, selectedGender);
            res = transact.AddRequest();
            editsID.Text = res;
            Toast.MakeText(activity, res, ToastLength.Long).Show();
        }

        public void BackHome()
        {
            Intent i = new Intent(activity, typeof(MainActivity));
            activity.StartActivity(i);
        }

        public void SearchRecord(string sID, string name, string school, string country, RadioGroup gender, string selectedGender, EditText editName, EditText editSchool, AutoCompleteTextView autoCompleteCountry)
        {

            transact = new Transact(ipAdd, sID, name, school, country, selectedGender);
            var u1 = transact.SearchRequest();

            string searchedname = u1.GetProperty("name").ToString();
            string searchedschool = u1.GetProperty("school").ToString();
            string searchedcountry = u1.GetProperty("country").ToString();
            string searchedgender = u1.GetProperty("gender").ToString();

            editName.Text = searchedname;
            editSchool.Text = searchedschool;
            autoCompleteCountry.Text = searchedcountry;

            if (searchedgender == "I dont want to specify")
            {
                gender.Check(Resource.Id.radioButton1);
            }
            else if (searchedgender == "MALE")
            {
                gender.Check(Resource.Id.radioButton2);
            }
            else if (searchedgender == "FEMALE")
            {
                gender.Check(Resource.Id.radioButton3);
            }
            Toast.MakeText(activity, searchedgender.ToString(), ToastLength.Long).Show();
        }

        public void UpdateRecord(string sID, string name, string school, string country, string selectedGender)
        {
            transact = new Transact(ipAdd, sID, name, school, country, selectedGender);
            transact.UpdateRequest();
        }
    }
}