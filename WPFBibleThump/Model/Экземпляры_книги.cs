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
    
    public partial class Экземпляры_книги
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Экземпляры_книги()
        {
            this.Выданные_книги = new HashSet<Выданные_книги>();
        }
    
        public string Инвентарный_номер { get; set; }
        public int Id_книги { get; set; }
    
        public virtual Книги Книги { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Выданные_книги> Выданные_книги { get; set; }
    }
}
