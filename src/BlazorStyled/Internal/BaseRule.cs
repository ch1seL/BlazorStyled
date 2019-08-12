﻿using BlazorStyled.Stylesheets;
using System;
using System.Collections.Generic;

namespace BlazorStyled.Internal
{
    internal abstract class BaseRule : IRule
    {
        protected readonly Hash _hashService = new Hash();
        private readonly List<Declaration> _declarations = new List<Declaration>();
        private readonly List<IRule> _rules = new List<IRule>();
        private string _hash;

        public string Selector { get; set; }
        public string Label { get; set; }

        public string Hash
        {
            get
            {
                if (_hash == null)
                {
                    SetHash();
                }
                return _hash;
            }
        }

        public IEnumerable<Declaration> Declarations => _declarations;

        public virtual RuleType RuleType => throw new NotImplementedException();

        public IEnumerable<IRule> NestedRules => _rules;

        public void AddDeclaration(Declaration declaration)
        {
            _declarations.Add(declaration);
            SetHash();
        }

        public void AddNestedRule(IRule rule)
        {
            _rules.Add(rule);
            SetHash();
        }

        public void SetHash()
        {
            _hash = _hashService.GetHashCode(this, Label);
        }

        public void AddDeclarations(IList<Declaration> declarations)
        {
            _declarations.AddRange(declarations);
            SetHash();
        }

        public void AddNestedRules(IList<IRule> rules)
        {
            _rules.AddRange(rules);
            SetHash();
        }

        public virtual void SetClassname()
        {
            //ignored
        }
    }
}