﻿using System;
using System.Windows.Forms;

namespace RasterEditor.Forms
{
    /// <summary>
    /// A dialog allowing user to input one single value.
    /// </summary>
    public partial class SingleInputDialog : System.Windows.Forms.Form
    {
        #region Delegate

        public delegate bool ValueValidate(string inputValue, out object validValue);

        #endregion

        #region Attribute

        private object value = null;

        private ValueValidate validateMethod = null;

        #endregion

        #region Property

        /// <summary>
        /// Set the name of the input value.
        /// </summary>
        public string ValueName
        {
            set { label1.Text = value; }
        }

        /// <summary>
        /// Get the input value.
        /// </summary>
        public object Value
        {
            get { return value; }
        }

        /// <summary>
        /// Set the value validation method.
        /// </summary>
        public ValueValidate ValueValidateMethod
        {
            set { validateMethod = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize the single input dialog.
        /// </summary>
        public SingleInputDialog()
        {
            InitializeComponent();

            FormClosing += new FormClosingEventHandler(SingleInputForm_FormClosing);
        }

        /// <summary>
        /// Initialize the single input dialog.
        /// </summary>
        /// <param name="valueName">Input value name.</param>
        public SingleInputDialog(string valueName)
            : this()
        {
            label1.Text = valueName;
        }

        /// <summary>
        /// Initialize the single input dialog.
        /// </summary>
        /// <param name="valueName">Input value name.</param>
        /// <param name="dialogCaption">Dailog caption.</param>
        public SingleInputDialog(string valueName,string dialogCaption)
            : this(valueName)
        {
            this.Text = dialogCaption;
        }

        #endregion

        #region Evnet

        // If the input fails, return DailogResult.Cancel.
        protected void SingleInputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
                this.DialogResult = DialogResult.Cancel;
        }

        // Click the okButton the confirm the input.
        protected void okButton_Click(object sender, EventArgs e)
        {
            if (inputTextBox.Text == "")
            {
                MessageBox.Show("Please input a value.", "Notice");
                return;
            }

            if (validateMethod != null)
            {
                object tValue = null;
                if (!validateMethod(inputTextBox.Text, out tValue))
                {
                    MessageBox.Show("Invalid input value.", "Notice");
                    return;
                }

                value = tValue;
            }
            else
            {
                value = inputTextBox.Text;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
