using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmetkaZaNaracka.SearchFilterForm
{
    public abstract class SortingState
    {
        public SearchFilter Form { get; set; }

        public SortingState(SearchFilter form)
        {
            Form = form;
        }

        public abstract void Evaluate();

        public abstract void Name();

        public abstract void Rejting();
    }
}
