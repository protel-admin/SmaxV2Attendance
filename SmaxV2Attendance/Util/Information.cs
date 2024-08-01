using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmaxV2Attendance.Util
{
    public class Information
    {
        public string Branch_Info { get; set; }
        public string Category_Info { get; set; }
        public string Company_Info { get; set; }
        public string Cardholder_Info { get; set; }
        public string Department_Info { get; set; }
        public string Designation_Info { get; set; }
        public string Holiday_Info { get; set; }
        public string Location_Info { get; set; }
        public string TimeZone_Info { get; set; }
        public string TimeZoneDetails_Info { get; set; }
        public string AccessLevel_Info { get; set; }
        public string AccessLevelDetails_Info { get; set; }
        public string Unit_Info { get; set; }

        public string Branch_Dup_Msg { get; set; }
        public string Category_Dup_Msg { get; set; }
        public string Company_Dup_Msg { get; set; }
        public string Cardholder_Dup_Msg { get; set; }
        public string Department_Dup_Msg { get; set; }
        public string Designation_Dup_Msg { get; set; }
        public string Holiday_Dup_Msg { get; set; }
        public string Location_Dup_Msg { get; set; }
        public string TimeZone_Dup_Msg { get; set; }
        public string TimeZoneDetails_Dup_Msg { get; set; }
        public string AccessLevel_Dup_Msg { get; set; }
        public string AccessLevelDetails_Dup_Msg { get; set; }
        public string Unit_Dup_Msg { get; set; }

        public Information()
        {
            Branch_Info = "This screen will alow you to add new Branch for the Units/Company.";
            Category_Info = "This screen will alow you to add new Category Name.";
            Company_Info = "This screen will alow you to add new Company";
            Cardholder_Info = "This screen will alow you to add new Cardholder";
            Department_Info = "This screen will alow you to add new Department";
            Designation_Info = "This screen will alow you to add new Designation";
            Holiday_Info = "This screen will alow you to add new Holiday";
            Location_Info = "This screen will alow you to add new Location";
            TimeZone_Info = "This screen will alow you to add new TimeZone";
            TimeZoneDetails_Info = "This screen will alow you to add TimeZoneDetails for a selected TimeZone";
            AccessLevel_Info = "This screen will alow you to add new AccessLevel";
            AccessLevelDetails_Info = "This screen will alow you to addAccess Level Details for a  selected Access Level";
            Unit_Info = "This screen will alow you to add new Unit";
            
            

            Branch_Dup_Msg = "Branch Name already exists. Please try another Branch Name.";
            Category_Dup_Msg = "Category Name already exists. Please try another Category Name.";
            Company_Dup_Msg = "Company Name already exists. Please try another Company Name.";
            Cardholder_Dup_Msg = "Employee Id already exists. Please try another Employee.";
            Designation_Dup_Msg = "Designation Name already exists. Please try another Designation Name.";
            Department_Dup_Msg = "Department Name already exists. Please try another Department Name.";
            Holiday_Dup_Msg = "Holiday Name already exists. Please try another Holiday Name.";
            Location_Dup_Msg = "Location Name already exists. Please try another Location Name.";
            TimeZone_Dup_Msg = "TimeZone Name already exists. Please try another TimeZone Name.";
            TimeZoneDetails_Dup_Msg = "TimeZoneDetails already exists.";
            AccessLevel_Dup_Msg = "AccessLevel already exists. Please try another AccessLevel.";
            AccessLevelDetails_Dup_Msg = "AccessLevelDetails already exists.";
            Unit_Dup_Msg = "Unit already exists.";
        }
    }
}