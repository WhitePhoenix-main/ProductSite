﻿namespace ProductsSite
{
    public interface IHasProduct
    {
        ProductRecord ProductRecord { get; set; }
        bool IsNewRec { get; set; }
    }
}