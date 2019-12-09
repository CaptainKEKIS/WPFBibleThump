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
    
    public partial class Книги
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Книги()
        {
            this.Авторы = new HashSet<Авторы>();
            this.Экземпляры_книги = new HashSet<Экземпляры_книги>();
        }
    
        public int Id { get; set; }
        public string Название { get; set; }
        public string Год_издания { get; set; }
        public int Id_издательства { get; set; }
        public int Id_города { get; set; }
        public string УДК { get; set; }
    
        public virtual Города Города { get; set; }
        public virtual Издательства Издательства { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Авторы> Авторы { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Экземпляры_книги> Экземпляры_книги { get; set; }
    }
}
