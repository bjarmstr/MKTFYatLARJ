using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Models.ViewModels.FAQ
{
    public class FAQVM
    {
        public FAQVM() { 
        
        }


        public FAQVM(Entities.FAQ src)
        {
            Id = src.Id;
            Question = src.Question;
            Answer = src.Answer;
           
        }

   
        public Guid Id { get; set; }


        public string Question { get; set; }


        public string Answer { get; set; }



    }
}
