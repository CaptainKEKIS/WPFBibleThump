using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFBibleThump.Model
{

    using System;
    using System.Collections.Generic;

    public partial class Авторы
    {

        public string FullName
        {
            get { return $"{Фамилия} {Имя} {Отчество}"; }
        }

    }

}
