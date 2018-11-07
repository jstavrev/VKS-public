using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMovieStore.Models.Contracts
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}
