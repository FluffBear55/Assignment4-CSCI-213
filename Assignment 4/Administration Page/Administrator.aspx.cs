using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment_4.Administration_Page {

    public partial class Administrator : System.Web.UI.Page {

        String emptyBoxMessage = "One or multiple boxes are empty.";
        String CriticalErrorMessage = "There was a critical error.";

        DataClasses1DataContext dbConn;

        protected void Page_Load(object sender, EventArgs e) {

            string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Alan\\Documents\\Assignment 4 CSCI 213 Repo\\Assignment4-CSCI-213\\Assignment 4\\App_Data\\KarateSchool.mdf\";Integrated Security=True;Connect Timeout=30";
            dbConn = new DataClasses1DataContext(connString);

            if (!IsPostBack) {

                var result = from item in dbConn.Instructors
                             orderby item.InstructorID
                             select item;
                InstructorGridView.DataSourceID = null;
                InstructorGridView.DataSource = result;
                InstructorGridView.DataBind();

                var result2 = from item in dbConn.Members
                             orderby item.Member_UserID
                             select item;
                MemberGridView.DataSourceID = null;
                MemberGridView.DataSource = result2;
                MemberGridView.DataBind();
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e) {

        }

        protected void SubmitButton_Click(object sender, EventArgs e) {

            if (CheckBoxes(FirstNameBox.Text, LastNameBox.Text, PhoneBox.Text, EmailBox.Text, PasswordBox.Text, UserNameBox.Text)) {

                BoxIsEmpty();
                return;
            }

            if (InstructorBox.Checked) {
                try {
                    // Create a new NetUser
                    NetUser netUser = new NetUser();
                    netUser.UserName = UserNameBox.Text;
                    netUser.UserPassword = PasswordBox.Text;
                    netUser.UserType = "Instructor";

                    // Insert the new NetUser into the database
                    dbConn.NetUsers.InsertOnSubmit(netUser);
                    dbConn.SubmitChanges(); // This ensures the UserID is generated

                    // Create a new Instructor
                    Instructor newInstructor = new Instructor();
                    newInstructor.InstructorID = netUser.UserID; // Use the generated UserID
                    newInstructor.InstructorFirstName = FirstNameBox.Text;
                    newInstructor.InstructorLastName = LastNameBox.Text;
                    newInstructor.InstructorPhoneNumber = PhoneBox.Text;

                    // Insert the new Instructor into the database
                    dbConn.Instructors.InsertOnSubmit(newInstructor);
                    dbConn.SubmitChanges();

                    var result = from item in dbConn.Instructors
                                 orderby item.InstructorID
                                 select item;
                    InstructorGridView.DataSource = result;
                    InstructorGridView.DataBind();
                }
                catch (Exception ex) {
                    // Handle exceptions, show error messages, log, etc.
                    MakeErrorVisible(CriticalErrorMessage);
                    MakeErrorVisible(ex.ToString());
                    Console.WriteLine(ex);
                }
            }

            if (!InstructorBox.Checked) {
                try {
                    // Create a new NetUser
                    NetUser netUser = new NetUser();
                    netUser.UserName = UserNameBox.Text;
                    netUser.UserPassword = PasswordBox.Text;
                    netUser.UserType = "Member";

                    // Insert the new NetUser into the database
                    dbConn.NetUsers.InsertOnSubmit(netUser);
                    dbConn.SubmitChanges(); // This ensures the UserID is generated

                    // Create a new Member
                    Member member = new Member();
                    member.Member_UserID = netUser.UserID; // Use the generated UserID
                    member.MemberFirstName = FirstNameBox.Text;
                    member.MemberLastName = LastNameBox.Text;
                    member.MemberPhoneNumber = PhoneBox.Text;
                    member.MemberEmail = EmailBox.Text;
                    member.MemberDateJoined = DateTime.Now;

                    // Insert the new Instructor into the database
                    dbConn.Members.InsertOnSubmit(member);
                    dbConn.SubmitChanges();

                    var result = from item in dbConn.Members
                                 orderby item.Member_UserID
                                 select item;
                    MemberGridView.DataSource = result;
                    MemberGridView.DataBind();
                }
                catch (Exception ex) {
                    // Handle exceptions, show error messages, log, etc.
                    MakeErrorVisible(CriticalErrorMessage);
                    MakeErrorVisible(ex.ToString());
                    Console.WriteLine(ex);
                }
            }

            //Response.Redirect(Request.RawUrl, false);
        }

        private bool CheckBoxes(params string[] values) {

            foreach (string value in values) {
                if (string.IsNullOrEmpty(value)) {

                    return true;
                }
            }
            return false;

        }

        private void BoxIsEmpty() {

            MakeErrorVisible(emptyBoxMessage);
        }

        private void MakeErrorVisible(string error) {

            ErrorLabel.Text = error;
            ErrorLabel.Visible = true;
        }
    }
}