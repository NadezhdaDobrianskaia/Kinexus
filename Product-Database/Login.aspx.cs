using System;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Security.Cryptography;

namespace Product_Database
{
    /// <summary>
    /// Kinexus login page
    /// </summary>
    public partial class Login : System.Web.UI.Page
    {
        /// <summary>
        /// Encrypt the string 
        /// </summary>
        /// <param name="value">The value to encrypt to sha3</param>
        /// <returns>The sha3 representation of the value</returns>
        private string encryptString(string value)
        {
            //initialize a sha3 service provider
            HashAlgorithm sha = new SHA384CryptoServiceProvider();

            //compute the sha hash of the byte representation of the string
            byte[] hashResult = sha.ComputeHash(ASCIIEncoding.Default.GetBytes(value));

            //return the string representation of the hash result
            return (Convert.ToBase64String(hashResult));
        }
        
        /// <summary>
        /// Attempt to login to the system
        /// </summary>
        /// <param name="username">Username to login with.</param>
        /// <param name="password">Password to login with.</param>
        /// <returns>True if the paramaters match the credentials; False otherwise.</returns>        
        private bool canLogin(string username, string password)
        {
            //login credentials encrypted with sha3
            string[] credentials = { "nKaUqQKFwDRDLJVQQht7nb1cD0tmc/BfbbzlgFK6IOQkgEGVbujJouyfECkM3AeC",    //username
                                     "myMBBTY0wOy43Q/OGJNN+XUx9tSJwcKsyKLVXR677CfrwPXoLNfrkTV9Cye4KnyD" };  //password

            //if the encryptyed usename and password match the credentials in that order
            if (encryptString(username).Equals(credentials[0]) && encryptString(password).Equals(credentials[1]))
            {
                //allow login
                return true;
            }

            //reject login
            return false;
        }

        /// <summary>
        /// Login control button pressed.
        /// </summary>
        /// <param name="sender">Login control.</param>
        /// <param name="e">Login event arguments.</param>
        protected void Login_Authenticate(object sender, AuthenticateEventArgs e)
        {
            //attempt to login to the system, and set the authenticated status to true if we succeed
            e.Authenticated = canLogin(login_control.UserName, login_control.Password);
            
            //if we were able to authenticate
            if (e.Authenticated)
            {
                //redirect back from this page, dont create a cookie
                FormsAuthentication.RedirectFromLoginPage(login_control.UserName, true);

                //redirect location
                Response.Redirect("Manager.aspx");
            }            
        }
    }
}