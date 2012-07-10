#region License
/*
<Project> <Project Description>
Copyright (c) <Year>, <Owner>
All rights reserved.
 
Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:
 
1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution.
3. Use of the source code or binaries for a competing project, whether open
   source or commercial, is prohibited unless permission is specifically
   granted under a separate license by <Owner>.
4. Source code enhancements or additions are the property of the author until
   the source code is contributed to this project. By contributing the source
   code to this project, the author immediately grants all rights to
   the contributed source code to <Owner>.
 
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
#endregion

using System;
using System.Diagnostics;
using System.Windows.Input;

namespace WPFSharp.Globalizer.Controls
{
    internal class RelayCommand : ICommand
    {
        #region Member variables
        readonly Action<object> _Execute;
        readonly Predicate<object> _CanExecute;
        #endregion Member variables

        #region Constructors
        /// <summary>
        /// Constructor for RelayCommand that only takes a method to run and
        /// the CanExecute method is null, which means it can always run.
        /// </summary>
        /// <param name="inMethodToExecute">The method to execute. Could be a lambda.</param>
        public RelayCommand(Action<object> inMethodToExecute)
            : this(inMethodToExecute, null)
        {
        }

        /// <summary>
        /// Constructor for RelayCommand that takes both a method to run 
        /// and a bool method to determine if it CanExecute.
        /// </summary>
        /// <param name="inMethodToExecute">The execution logic.</param>
        /// <param name="inCanExecute">The execution status logic.</param>
        public RelayCommand(Action<object> inMethodToExecute, Predicate<object> inCanExecute)
        {
            if (inMethodToExecute == null)
                throw new ArgumentNullException("inMethodToExecute");

            _Execute = inMethodToExecute;
            _CanExecute = inCanExecute;
        }
        #endregion Constructors

        #region ICommand Members
        [DebuggerStepThrough]
        public bool CanExecute(object inParameter)
        {
            return _CanExecute == null || _CanExecute(inParameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Run the method assigned to the RelayCommand with the parameters
        /// passed in.
        /// </summary>
        /// <param name="inParameter"></param>
        public void Execute(object inParameter)
        {
            _Execute(inParameter);
        }

        /// <summary>
        /// This will cause the CanExecute to be re-evaluated and is useful 
        /// when a button doesn't re-enabled, often due to a thread or 
        /// BackgroundWorker.
        /// </summary>
        public void EvaluateCanExecute()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        #endregion ICommand Members
    }
}