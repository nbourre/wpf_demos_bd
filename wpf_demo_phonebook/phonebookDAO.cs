using System;
using System.Collections.Generic;
using System.Text;

namespace wpf_demo_phonebook
{
    class phonebookDAO
    {
        private DbConnection conn;

        public phonebookDAO()
        {
            conn = new DbConnection();
        }
    }
}
