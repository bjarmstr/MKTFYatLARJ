using MKTFY.Models.ViewModels.FAQ;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Models.Entities
{
   public class FAQ
    {
        public FAQ()
        {

        }

        public FAQ(FAQCreateVM src)
        {
            Question = src.Question;
            Answer = src.Answer;
           

        }

        public FAQ(FAQUpdateVM src)
        {
            Id = src.Id;
            Question = src.Question;
            Answer = src.Answer;

        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Question { get; set; }

        [Required]
        public string Answer { get; set; }

        
        public DateTime DateCreated { get; set; }



    }
}
