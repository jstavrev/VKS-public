using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMovieStore.Models.Contracts
{
    public interface IAuditable
    {
        DateTime? CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
