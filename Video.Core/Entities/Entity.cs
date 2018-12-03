using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using Video.Core.Interface;
using Video.Infrastructrue.Services;

namespace Video.Core.Entities
{
   public abstract class  Entity
    {
         

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int Id { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        
        
    }
}
