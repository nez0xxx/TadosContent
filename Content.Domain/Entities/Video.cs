﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Content.Domain.Enums;

namespace Content.Domain.Entities
{
    public class Video : Content
    {
        [Obsolete("Only for reflection", true)]
        public Video()
        {
        }
        
        protected internal Video(string title, string url, User creator) : base(ContentType.Video, title, creator)
        {
            Url = url;
        }

        public virtual string Url { get; protected set; }
    }
}
