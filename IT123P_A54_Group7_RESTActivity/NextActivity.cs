using Android.App;
using Android.Content;
using Android.Icu.Text;
using Android.OS;
using Android.Widget;
using System;
using System.IO;
using System.Net;
using System.Text.Json;

namespace MyRESTfulDB
{
    [Activity(Label = "NextActivity")]
    public class NextActivity : Activity
    {
        EditText editName, editSchool, editsID;
        Button btnAdd, btnSearch, btnUpdate, btnHome;
        RadioGroup gender;
        AutoCompleteTextView autoCompleteCountry;
        Function func;
        String selectedGender = "";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.nextlayout);

            func = new Function(this);

            editName = FindViewById<EditText>(Resource.Id.editText1);
            editSchool = FindViewById<EditText>(Resource.Id.editText2);
            editsID = FindViewById<EditText>(Resource.Id.sID);
            btnAdd = FindViewById<Button>(Resource.Id.button1);
            btnSearch = FindViewById<Button>(Resource.Id.button2);
            btnUpdate = FindViewById<Button>(Resource.Id.button3);
            btnHome = FindViewById<Button>(Resource.Id.button4);
            gender = FindViewById<RadioGroup>(Resource.Id.radioGroup1);

            autoCompleteCountry = FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteTextView1);
            var country = new string[] { "Cambodia", "Indonesia", "Philippines", "Thailand", "Singapore" };
            ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, country);
            autoCompleteCountry.Adapter = adapter;

            gender.CheckedChange += myRadioGroup_CheckedChange;

            btnAdd.Click += delegate
            {func.AddRecord(editName.Text, editSchool.Text, autoCompleteCountry.Text, selectedGender, editsID);};

            btnHome.Click += delegate
            {func.BackHome();};

            btnSearch.Click += delegate
            {func.SearchRecord(editsID.Text, editName.Text, editSchool.Text, autoCompleteCountry.Text, gender, selectedGender, editName, editSchool, autoCompleteCountry);};

            btnUpdate.Click += delegate 
            {func.UpdateRecord(editsID.Text, editName.Text, editSchool.Text, autoCompleteCountry.Text, selectedGender);};
        }

        void myRadioGroup_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            int checkedItemId = gender.CheckedRadioButtonId;
            RadioButton checkedRadioButton = FindViewById<RadioButton>(checkedItemId);
            selectedGender = checkedRadioButton.Text;
        }

    }
}