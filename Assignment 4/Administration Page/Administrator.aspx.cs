using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment_4.Administration_Page {

    public partial class Administrator : System.Web.UI.Page {

        // Error messages.
        String emptyBoxMessage = "One or multiple boxes are empty.";
        String CriticalErrorMessage = "There was a critical error.";

        DataClasses1DataContext dbConn;

        // On Page Load
        protected void Page_Load(object sender, EventArgs e) {

            //Connect to DB
            string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";
            dbConn = new DataClasses1DataContext(connString);

            // If !IsPostBack then update tables.
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

        // Deleting users.
        protected void InstructorGridView_RowCommand(object sender, GridViewCommandEventArgs e) {

            if (e.CommandName == "DeleteUser") {

                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int instructorID = Convert.ToInt32(InstructorGridView.DataKeys[rowIndex].Value);

                // Call a method to delete the record based on the instructorID
                DeleteInstructor(instructorID);

                // Rebind the GridView to refresh the data
                BindInstructorGridView();
            }
        }

        // Deleting users for MemberGridView.
        protected void MemberGridView_RowCommand(object sender, GridViewCommandEventArgs e) {
            if (e.CommandName == "DeleteUser") {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int memberID = Convert.ToInt32(MemberGridView.DataKeys[rowIndex].Value);

                // Call a method to delete the record based on the memberID
                DeleteMember(memberID);

                // Rebind the GridView to refresh the data
                BindMemberGridView();
            }
        }

        // Update Instructor Table
        protected void BindInstructorGridView() {

            var result = from item in dbConn.Instructors
                         orderby item.InstructorID
                         select item;

            InstructorGridView.DataSource = result;
            InstructorGridView.DataBind();
        }

        //Update Member Table
        protected void BindMemberGridView() {
            var result = from member in dbConn.Members
                         orderby member.Member_UserID
                         select member;

            MemberGridView.DataSource = result;
            MemberGridView.DataBind();
        }

        protected void DeleteInstructor(int instructorID) {
            
            // Step 1: Delete the Instructor
            var instructorToDelete = dbConn.Instructors.SingleOrDefault(i => i.InstructorID == instructorID);
            if (instructorToDelete != null) {
                dbConn.Instructors.DeleteOnSubmit(instructorToDelete);
                dbConn.SubmitChanges();
            }

            // Step 2: Delete associated NetUser
            var netUserToDelete = dbConn.NetUsers.SingleOrDefault(u => u.UserID == instructorID);
            if (netUserToDelete != null) {
                dbConn.NetUsers.DeleteOnSubmit(netUserToDelete);
                dbConn.SubmitChanges();
            }
        }

        protected void DeleteMember(int memberID) {

            // Step 1: Delete the Member
            var memberToDelete = dbConn.Members.SingleOrDefault(m => m.Member_UserID == memberID);
            if (memberToDelete != null) {
                dbConn.Members.DeleteOnSubmit(memberToDelete);
                dbConn.SubmitChanges();
            }

            // Step 2: Delete associated NetUser
            var netUserToDelete = dbConn.NetUsers.SingleOrDefault(u => u.UserID == memberID);
            if (netUserToDelete != null) {
                dbConn.NetUsers.DeleteOnSubmit(netUserToDelete);
                dbConn.SubmitChanges();
            }
        }

        // Not deleting this in case it causes issues.
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
                    MakeErrorVisible(CriticalErrorMessage + ": " + ex.ToString());
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

        // Checks all text boxes to make sure they're not blank.
        private bool CheckBoxes(params string[] values) {

            foreach (string value in values) {
                if (string.IsNullOrEmpty(value)) {

                    return true;
                }
            }
            return false;

        }

        // If box is empty, make an error.
        private void BoxIsEmpty() {

            MakeErrorVisible(emptyBoxMessage);
        }

        // Makes Error Label Visible
        private void MakeErrorVisible(string error) {

            ErrorLabel.Text = error;
            ErrorLabel.Visible = true;
        }

        // Not deleting this in case it causes issues.
        protected void MemberGridView_SelectedIndexChanged(object sender, EventArgs e) {

        }
    }
}