﻿using System;
using Nest.DAL;
using Nest.ViewModels;

namespace Nest.Services
{
    public class LayoutService
    {
        NestContext _context { get; }
        public LayoutService(NestContext context)
        {
            _context = context;
        }


        public Dictionary<string, string> GetSettings()
        {
            return _context.Settings.ToDictionary(p => p.Key, p => p.Value);
        }
    }
}

