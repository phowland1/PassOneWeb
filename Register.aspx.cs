﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PassOne.Business;
using PassOne.Domain;

namespace PassOne
{
    public partial class Register : System.Web.UI.Page
    {
        public static string Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PassOneWeb\\";
        UserManager _userManager = new UserManager();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            try
            {
                _userManager.CreateUser(Path, FirstNameTextBox.Text, LastNameTextBox.Text, UsernameTextBox.Text, PasswordTextBox.Text);
                Session["User"] = _userManager.Authenticate(UsernameTextBox.Text, PasswordTextBox.Text, Path);
                Response.Redirect("CredentialsList.aspx");
            }
            catch (MissingInformationException)
            {
               
            }
        }
    }
}