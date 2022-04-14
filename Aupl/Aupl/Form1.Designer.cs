using Antlr4.Runtime;
using Aupl;
using Aupl.Content;

namespace Aupl
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            /// <summary>
               /// Required method for Designer support - do not modify
               /// the contents of this method with the code editor.
               /// </summary>
             
                   this.MainLabel = new System.Windows.Forms.Label();
                   this.ChooseFileBtn = new System.Windows.Forms.Button();
                   this.FileChosenLbl = new System.Windows.Forms.Label();
                   this.FileChosenEditedLbl = new System.Windows.Forms.Label();
                   this.richTextBox1 = new System.Windows.Forms.RichTextBox();
                   this.errorText = new System.Windows.Forms.Label();
                   this.SuspendLayout();
                   // 
                   // MainLabel
                   // 
                   this.MainLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
                   this.MainLabel.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
                   this.MainLabel.Location = new System.Drawing.Point(198, 9);
                   this.MainLabel.Name = "MainLabel";
                   this.MainLabel.Size = new System.Drawing.Size(403, 50);
                   this.MainLabel.TabIndex = 0;
                   this.MainLabel.Text = "AUPL Compiler\r\n";
                   this.MainLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
                   this.MainLabel.Click += new System.EventHandler(this.label1_Click);
                   // 
                   // ChooseFileBtn
                   // 
                   this.ChooseFileBtn.BackColor = System.Drawing.SystemColors.Highlight;
                   this.ChooseFileBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                   this.ChooseFileBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
                   this.ChooseFileBtn.Location = new System.Drawing.Point(288, 62);
                   this.ChooseFileBtn.Name = "ChooseFileBtn";
                   this.ChooseFileBtn.Size = new System.Drawing.Size(184, 43);
                   this.ChooseFileBtn.TabIndex = 1;
                   this.ChooseFileBtn.Text = "Choose a file";
                   this.ChooseFileBtn.UseVisualStyleBackColor = false;
                   this.ChooseFileBtn.Click += new System.EventHandler(this.ChooseFileBtn_Click);
                   // 
                   // FileChosenLbl
                   // 
                   this.FileChosenLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
                   this.FileChosenLbl.Location = new System.Drawing.Point(12, 131);
                   this.FileChosenLbl.Name = "FileChosenLbl";
                   this.FileChosenLbl.Size = new System.Drawing.Size(147, 28);
                   this.FileChosenLbl.TabIndex = 2;
                   this.FileChosenLbl.Text = "File Chosen:";
                   this.FileChosenLbl.Click += new System.EventHandler(this.label1_Click_1);
                   // 
                   // FileChosenEditedLbl
                   // 
                   this.FileChosenEditedLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
                   this.FileChosenEditedLbl.Location = new System.Drawing.Point(148, 128);
                   this.FileChosenEditedLbl.Name = "FileChosenEditedLbl";
                   this.FileChosenEditedLbl.Size = new System.Drawing.Size(324, 31);
                   this.FileChosenEditedLbl.TabIndex = 3;
                   this.FileChosenEditedLbl.Text = "No file selected";
                   this.FileChosenEditedLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                   this.FileChosenEditedLbl.Click += new System.EventHandler(this.label1_Click_2);
                   // 
                   // richTextBox1
                   // 
                   this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
                   this.richTextBox1.Location = new System.Drawing.Point(29, 172);
                   this.richTextBox1.Name = "richTextBox1";
                   this.richTextBox1.Size = new System.Drawing.Size(759, 266);
                   this.richTextBox1.TabIndex = 4;
                   this.richTextBox1.Text = "";
                   // 
                   // errorText
                   // 
                   this.errorText.BackColor = System.Drawing.SystemColors.ActiveBorder;
                   this.errorText.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
                   this.errorText.ForeColor = System.Drawing.Color.Red;
                   this.errorText.Location = new System.Drawing.Point(288, 228);
                   this.errorText.Name = "errorText";
                   this.errorText.Size = new System.Drawing.Size(223, 49);
                   this.errorText.TabIndex = 5;
                   this.errorText.Text = "errorText";
                   // 
                   // Form1
                   // 
                   this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                   this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                   this.BackColor = System.Drawing.SystemColors.GrayText;
                   this.ClientSize = new System.Drawing.Size(800, 450);
                   this.Controls.Add(this.richTextBox1);
                   this.Controls.Add(this.FileChosenEditedLbl);
                   this.Controls.Add(this.FileChosenLbl);
                   this.Controls.Add(this.ChooseFileBtn);
                   this.Controls.Add(this.MainLabel);
                   this.Controls.Add(this.errorText);
                   this.Name = "Form1";
                   this.Text = "Form1";
                   this.Load += new System.EventHandler(this.Form1_Load);
                   this.ResumeLayout(false);
               }
               private void ChooseFileBtn_Click(object sender, EventArgs e)
               {
                   OpenFileDialog fileDialog = new OpenFileDialog();
                   fileDialog.Multiselect = false;
                   // fileDialog.Filter = ".aupl";
                   // fileDialog.DefaultExt = ".aupl";
       
                   fileDialog.ShowDialog();
                   if (fileDialog.FileName != "")
                   {
                       FileChosenEditedLbl.Text = fileDialog.FileName;
                       var filename = fileDialog.FileName;
                       //var filename = args[0];

                       var fileContents = File.ReadAllText(filename);


                       var inputStream = new AntlrInputStream(fileContents);
                       var auplLexer = new AuplLexer(inputStream);
                       var commonTokenStream = new CommonTokenStream(auplLexer);
                       var auplParser = new AuplParser(commonTokenStream);
                       var auplContext = auplParser.program();
                       var visitor = new AuplVisitor();
                       visitor.Visit(auplContext);

                       if (AuplVisitor.SemanticError.Count > 0)
                       {
                           //errorText.Text = AuplVisitor.SemanticError.First().ToUpper();
                           richTextBox1.Text =  "Error on line:" + AuplVisitor.SemanticError.First().Value + " " + AuplVisitor.SemanticError.First().Key;
                           //this.errorText.ForeColor = System.Drawing.Color.Red;
                       }
                       else
                       {
                           var sb = new System.Text.StringBuilder();

                           foreach (var output in AuplVisitor.Output)
                           {
                               sb.AppendLine(output.ToString());
                           }
                           richTextBox1.Text = sb.ToString();
                       }
                   }
                   else
                   {
                       FileChosenEditedLbl.Text = "No file selected";
                   }
                   
                   
               }
               
               private System.Windows.Forms.Label errorText;
               
               private System.Windows.Forms.RichTextBox richTextBox1;
       
               private System.Windows.Forms.Label FileChosenLbl;
       
               private System.Windows.Forms.Button ChooseFileBtn;
       
               private System.Windows.Forms.Label MainLabel;
       
               private System.Windows.Forms.Label FileChosenEditedLbl;
               
               
       
              
               
               private void label1_Click(object sender, EventArgs e)
               {
                   
               }
       
               private void Form1_Load(object sender, EventArgs e)
               {
                   
               }
       
               private void label1_Click_1(object sender, EventArgs e)
               {
                  
               }
               
               private void textBox1_TextChanged(object sender, EventArgs e)
               {
                   throw new System.NotImplementedException();
               }
       
               private void label1_Click_2(object sender, EventArgs e)
               {
                   
               }
        

        #endregion
    }
}