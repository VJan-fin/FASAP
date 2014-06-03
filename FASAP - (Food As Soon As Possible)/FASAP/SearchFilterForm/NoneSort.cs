using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmetkaZaNaracka.Properties;

namespace SmetkaZaNaracka.SearchFilterForm
{
    public class NoneSort : SortingState
    {
        public NoneSort(SearchFilter form)
            : base(form)
        { }

        public override void Name()
        {
            Form.SortingState = Form.NameDescendingSort;
            Form.pbIme.Image = Resources.LightArrowDown;
            Form.SortingState.Evaluate();
        }

        public override void Rejting()
        {
            Form.SortingState = Form.RejtingDescendingSort;
            Form.pbRejting.Image = Resources.LightArrowDown;
            Form.SortingState.Evaluate();
        }

        public override void Evaluate()
        {
            throw new NotImplementedException();
        }
    }
}
