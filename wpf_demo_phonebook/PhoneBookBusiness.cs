using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wpf_demo_phonebook
{
    class PhoneBookBusiness
    {
        private static PhonebookDAO dao = new PhonebookDAO();

        public static ContactModel GetContactByName(string _name)
        {
            ContactModel cm = new ContactModel();

            DataTable dt = new DataTable();

            dt = dao.SearchByName(_name);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cm.ContactID = Convert.ToInt32(row["ContactID"]);
                    cm.FirstName = row["FirstName"].ToString();
                    cm.LastName = row["LastName"].ToString();
                    cm.Email = row["Email"].ToString();
                    cm.Phone = row["Phone"].ToString();
                    cm.Mobile = row["Mobile"].ToString();
                }
            }

            return cm;
        }
    }
}
