﻿using System;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Attributes
{
    public class DescriptionAttribute : Attribute
    {
        public DescriptionAttribute(string description) => Description = description;

        public string Description { get; private set; }
    }
}
