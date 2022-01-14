Public Class Form1

    'store file
    Dim filename As String = ""

    'mark current document is dirty 
    Dim dirty As Boolean = False

    Private Sub rtbContent_Resize(sender As Object, e As EventArgs) Handles rtbContent.Resize

        'resize textbox 
        rtbContent.Size = New Size(Me.ClientSize.Width, Me.ClientSize.Height - tsMain.Height - msMain.Height)


    End Sub

    Private Sub rtbContent_TextChanged(sender As Object, e As EventArgs) Handles rtbContent.TextChanged, rtbContent.StyleChanged, rtbContent.SizeChanged

        'set docement as dirty 
        dirty = True

    End Sub

    Private Sub CutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem.Click

        rtbContent.Cut()

    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        rtbContent.Copy()
    End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        rtbContent.Paste()
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectAllToolStripMenuItem.Click
        rtbContent.SelectAll()
    End Sub

    Sub style(style As FontStyle)

        'store current font style
        Dim current As FontStyle = rtbContent.SelectionFont.Style

        If style = FontStyle.Bold Then
            'check if bold is off
            If rtbContent.SelectionFont.Bold = False Then

                'add bold
                current += FontStyle.Bold

            Else

                'take away bold
                current -= FontStyle.Bold
                rtbContent.SelectionFont = New Font(rtbContent.SelectionFont, FontStyle.Regular)

            End If

            'replace current font with new font style
            rtbContent.SelectionFont = New Font(rtbContent.SelectionFont, current)

        ElseIf style = FontStyle.Italic Then

            'check if Italic is off
            If rtbContent.SelectionFont.Italic = False Then

                'add Italic
                current += FontStyle.Italic

            Else

                'take away Italic
                current -= FontStyle.Italic
                rtbContent.SelectionFont = New Font(rtbContent.SelectionFont, FontStyle.Regular)

            End If

            'replace current font with new font style
            rtbContent.SelectionFont = New Font(rtbContent.SelectionFont, current)

        ElseIf style = FontStyle.Underline Then

            'check if Underline is off
            If rtbContent.SelectionFont.Underline = False Then

                'add Underline
                current += FontStyle.Underline

            Else

                'take away Underline
                current -= FontStyle.Underline
                rtbContent.SelectionFont = New Font(rtbContent.SelectionFont, FontStyle.Regular)

            End If

            'replace current font with new font style
            rtbContent.SelectionFont = New Font(rtbContent.SelectionFont, current)

        ElseIf style = FontStyle.Strikeout Then

            'check if Strikeout is off
            If rtbContent.SelectionFont.Strikeout = False Then

                'add Strikeout
                current += FontStyle.Strikeout

            Else

                'take away Strikeout
                current -= FontStyle.Strikeout
                rtbContent.SelectionFont = New Font(rtbContent.SelectionFont, FontStyle.Regular)

            End If

            'replace current font with new font style
            rtbContent.SelectionFont = New Font(rtbContent.SelectionFont, current)

        End If

    End Sub


    Private Sub tsbBold_Click(sender As Object, e As EventArgs) Handles tsbBold.Click
        style(FontStyle.Bold)
    End Sub

    Private Sub tsbItalic_Click(sender As Object, e As EventArgs) Handles tsbItalic.Click
        style(FontStyle.Italic)
    End Sub

    Private Sub tsbUnderline_Click(sender As Object, e As EventArgs) Handles tsbUnderline.Click
        style(FontStyle.Underline)
    End Sub

    Private Sub tsbStrikeCut_Click(sender As Object, e As EventArgs) Handles tsbStrikeCut.Click
        style(FontStyle.Strikeout)
    End Sub

    Private Sub tscSize_TextChanged(sender As Object, e As EventArgs) Handles tscSize.TextChanged

        'take number and convert into single
        Dim newsize As Single = Convert.ToSingle(tscSize.Text)

        'Apply new size
        rtbContent.SelectionFont = New Font(rtbContent.SelectionFont.FontFamily, newsize)



    End Sub

    Private Sub PictureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PictureToolStripMenuItem.Click

        'set file filter
        OpenFile.Filter = "Image Files|*.jpg;*.png;*.gif;*.tiff"

        'if user press OK
        If OpenFile.ShowDialog = Windows.Forms.DialogResult.OK Then

            'load picture
            Dim img As Image = Image.FromFile(OpenFile.FileName)

            'copy into clipboard and save to textbox
            Clipboard.SetImage(img)
            rtbContent.Paste()
        End If



    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click

        'open save file dialog
        If SaveFile.ShowDialog = Windows.Forms.DialogResult.OK Then

            'save file
            rtbContent.SaveFile(SaveFile.FileName)
            dirty = False

            'remember the file name 
            filename = SaveFile.FileName

            'set form title to the filename
            Me.Text = IO.Path.GetFileName(filename)

        End If

    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click

        'check if file have been save
        If filename = "" Then

            'call on the save as button
            SaveAsToolStripMenuItem_Click(Nothing, Nothing)

        Else

            'save file
            rtbContent.SaveFile(SaveFile.FileName)
            dirty = False

        End If

    End Sub
    Sub checkdirty()

        'check if the file have been save or not
        If dirty = True Then

            If MessageBox.Show("Do you want to save ?", "Confirm?", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then

                SaveAsToolStripMenuItem_Click(Nothing, Nothing)

            End If

        End If

    End Sub
    Private Sub QuitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QuitToolStripMenuItem.Click

        checkdirty()

        'close form
        Me.Close()

    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        checkdirty()

        'setup new text box 
        filename = ""
        dirty = False
        Me.Text = "Word Processor"
        rtbContent.ResetText()

    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click

        dirty = False
        checkdirty()

        OpenFile.Filter = "RTF document|*.rtf"

        If OpenFile.ShowDialog = Windows.Forms.DialogResult.OK Then

            rtbContent.LoadFile(OpenFile.FileName)
            dirty = False
            filename = OpenFile.FileName
            Me.Text = IO.Path.GetFileName(filename)

        End If

    End Sub

    Private Sub tsbNew_Click(sender As Object, e As EventArgs) Handles tsbNew.Click

        NewToolStripMenuItem_Click(Nothing, Nothing)

    End Sub

    Private Sub tsbOpen_Click(sender As Object, e As EventArgs) Handles tsbOpen.Click

        OpenToolStripMenuItem_Click(Nothing, Nothing)

    End Sub

    Private Sub tsbSave_Click(sender As Object, e As EventArgs) Handles tsbSave.Click

        SaveToolStripMenuItem_Click(Nothing, Nothing)

    End Sub
End Class
