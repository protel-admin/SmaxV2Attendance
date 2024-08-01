using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmaxV2Attendance.Models
{
    public class EmployeeListItemDto
    {

    public bool IsSelected { get; set; }   
    public string  EmployeeId {get;set;}
    public string  EmployeeName {get;set;}
    public string  CSN  {get;set;}
    public string  Department  {get;set;}
    public string  Designation  {get;set;}
    public string  Unit  {get;set;}
    public EmployeeListItemDto()
    {

        IsSelected = false;
    }
    
    }  
    
 
    
}