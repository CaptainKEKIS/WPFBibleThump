
namespace WPFBibleThump.Model
{
    using System;
    using System.Collections.Generic;

    public partial class Книги
    {
        public int CopiesCount
        {
            get
            {
                return Экземпляры_книги.Count;
            }
        }
    }
}
