//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Result
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int Dimension { get; set; }
        public int Opponent { get; set; }
        public int Result1 { get; set; }
    
        public virtual ResultsName ResultsName { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual Dimension Dimension1 { get; set; }
    }
}