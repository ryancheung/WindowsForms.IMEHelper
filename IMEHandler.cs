﻿using System;
using System.Windows.Forms;

namespace WindowsForms.IMEHelper
{
    /// <summary>
    /// Integrate IME to XNA framework.
    /// </summary>
    public class IMEHandler : IDisposable
    {
        private IMENativeWindow _nativeWnd;
        public Form Form1;

        /// <summary>
        /// Constructor. Must be called in <see cref="Microsoft.Xna.Framework.Game.Initialize()"/> function.
        /// </summary>
        /// <param name="game">Game instance</param>
        /// <param name="showDefaultIMEWindow">Should display system IME windows.</param>
        public IMEHandler(Form form, bool showDefaultIMEWindow = false)
        {
            Form1 = form;
            _nativeWnd = new IMENativeWindow(form.Handle, showDefaultIMEWindow);
            _nativeWnd.CandidatesReceived += (s, e) => { if (CandidatesReceived != null) CandidatesReceived(s, e); };
            _nativeWnd.CompositionReceived += (s, e) => { if (CompositionReceived != null) CompositionReceived(s, e); };
            _nativeWnd.ResultReceived += (s, e) => { if (ResultReceived != null) ResultReceived(s, e); };

            Form1.FormClosing += (o, e) => this.Dispose();
        }

        /// <summary>
        /// Called when the candidates updated
        /// </summary>
        public event EventHandler CandidatesReceived;

        /// <summary>
        /// Called when the composition updated
        /// </summary>
        public event EventHandler CompositionReceived;

        /// <summary>
        /// Called when a new result character is coming
        /// </summary>
        public event EventHandler<IMEResultEventArgs> ResultReceived;

        /// <summary>
        /// Array of the candidates
        /// </summary>
        public string[] Candidates { get { return _nativeWnd.Candidates; } }

        /// <summary>
        /// How many candidates should display per page
        /// </summary>
        public uint CandidatesPageSize { get { return _nativeWnd.CandidatesPageSize; } }

        /// <summary>
        /// First candidate index of current page
        /// </summary>
        public uint CandidatesPageStart { get { return _nativeWnd.CandidatesPageStart; } }

        /// <summary>
        /// The selected canddiate index
        /// </summary>
        public uint CandidatesSelection { get { return _nativeWnd.CandidatesSelection; } }

        /// <summary>
        /// Composition String
        /// </summary>
        public string Composition { get { return _nativeWnd.CompositionString; } }

        /// <summary>
        /// Composition Clause
        /// </summary>
        public string CompositionClause { get { return _nativeWnd.CompositionClause; } }

        /// <summary>
        /// Composition Reading String
        /// </summary>
        public string CompositionRead { get { return _nativeWnd.CompositionReadString; } }

        /// <summary>
        /// Composition Reading Clause
        /// </summary>
        public string CompositionReadClause { get { return _nativeWnd.CompositionReadClause; } }

        /// <summary>
        /// Caret position of the composition
        /// </summary>
        public int CompositionCursorPos { get { return _nativeWnd.CompositionCursorPos; } }

        /// <summary>
        /// Result String
        /// </summary>
        public string Result { get { return _nativeWnd.ResultString; } }

        /// <summary>
        /// Result Clause
        /// </summary>
        public string ResultClause { get { return _nativeWnd.ResultClause; } }

        /// <summary>
        /// Result Reading String
        /// </summary>
        public string ResultRead { get { return _nativeWnd.ResultReadString; } }

        /// <summary>
        /// Result Reading Clause
        /// </summary>
        public string ResultReadClause { get { return _nativeWnd.ResultReadClause; } }

        /// <summary>
        /// Enable / Disable IME
        /// </summary>
        public bool Enabled
        {
            get
            {
                return _nativeWnd.IsEnabled;
            }
            set
            {
                if (value)
                    _nativeWnd.EnableIME();
                else
                    _nativeWnd.DisableIME();
            }
        }

        /// <summary>
        /// Get the composition attribute at character index.
        /// </summary>
        /// <param name="index">Character Index</param>
        /// <returns>Composition Attribute</returns>
        public CompositionAttributes GetCompositionAttr(int index)
        {
            return _nativeWnd.GetCompositionAttr(index);
        }

        /// <summary>
        /// Get the composition read attribute at character index.
        /// </summary>
        /// <param name="index">Character Index</param>
        /// <returns>Composition Attribute</returns>
        public CompositionAttributes GetCompositionReadAttr(int index)
        {
            return _nativeWnd.GetCompositionReadAttr(index);
        }

        /// <summary>
        /// Dispose everything
        /// </summary>
        public void Dispose()
        {
            _nativeWnd.Dispose();
        }
    }
}
