﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace Assignment_4
{
    public partial class Logon : System.Web.UI.Page
    {
        //Connect to the database
        KarateDataContext dbcon;
        string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\mbaet\\Documents\\GitHub\\Assignment4-CSCI-213\\Assignment 4\\Databases\\KarateSchool.mdf\";Integrated Security=True;Connect Timeout=30";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Initialize connection string 
            dbcon = new KarateDataContext(conn);
        }


        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            string nUserName = Login1.UserName;
            string nPassword = Login1.Password;


            HttpContext.Current.Session["nUserName"] = nUserName;
            HttpContext.Current.Session["uPass"] = nPassword;


            // Search for the current User, validate UserName and Password

            NetUser myUser = (from x in dbcon.NetUsers
                              where x.UserName == HttpContext.Current.Session["nUserName"].ToString()
                              && x.UserPassword == HttpContext.Current.Session["uPass"].ToString()
                              select x).FirstOrDefault();

            if (myUser != null)
            {
                //Add UserID and User type to the Session
                HttpContext.Current.Session["userID"] = myUser.UserID;
                HttpContext.Current.Session["userType"] = myUser.UserType;

            }
            if (myUser != null && HttpContext.Current.Session["userType"].ToString().Trim() == "member")
            {

                FormsAuthentication.RedirectFromLoginPage(HttpContext.Current.Session["nUserName"].ToString(), true);

                Response.Redirect("C:\\Users\\mbaet\\Documents\\GitHub\\Assignment4-CSCI-213\\Assignment 4\\StudentInfo\\StudentInfo.aspx.cs");
            }
            else if (myUser != null && HttpContext.Current.Session["userType"].ToString().Trim() == "instructor")
            {

                FormsAuthentication.RedirectFromLoginPage(HttpContext.Current.Session["nUserName"].ToString(), true);

                Response.Redirect("C:\\Users\\mbaet\\Documents\\GitHub\\Assignment4-CSCI-213\\Assignment 4\\InstructorInfo\\InstructorInfo.aspx.cs");
            }
            else
                Response.Redirect("Logon.aspx", true);
        }
    }
}



