//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPFBibleThump.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Выданные_книги
    {
        public int Id_читателя { get; set; }
        public string Инвентарный_номер { get; set; }
        public System.DateTime Дата_выдачи { get; set; }
        public Nullable<System.DateTime> Дата_возврата { get; set; }
        public string Код_УДК { get; set; }
        public int Id { get; set; }
    
        public virtual Читатели Читатели { get; set; }
        public virtual Экземпляры_книги Экземпляры_книги { get; set; }
    }
}
