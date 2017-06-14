Imports System.IO
Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim command As String
        Dim desiredline As Integer
        Dim found As Boolean
        Dim commandLength As Integer
        Dim tempString As String
        Dim dictionaries As String()
        dictionaries = Directory.GetFiles(Application.StartupPath & "\Dictionaries\")
        ListBox1.Items.AddRange(dictionaries)
        command = TextBox1.Text
        If command.ToLower.Contains("что значит") Or command.ToLower.Contains("что такое") Then
            command = command.ToLower.Replace("что значит", "")
            command = command.ToLower.Replace("что такое", "")
            command = command.ToLower.Replace("?", "")
            command = command.ToLower.Replace(" ", "")
            commandLength = command.Length
            'MsgBox(command)
            Dim array1() As String
            For Each element As String In dictionaries
                Dim fi As New FileInfo(element)
                'MsgBox(fi.Name)
                array1 = File.ReadAllLines(element, System.Text.Encoding.Default)
                For i = 0 To array1.Length - 1
                    If array1(i).Contains(command.ToUpper) Then
                        tempString = array1(i)
                        Dim tempLength As Integer
                        tempLength = tempString.Length
                        tempString = tempString.Remove(commandLength + 5, tempLength - commandLength - 5)
                        'MsgBox(tempString)
                        If tempString = "     " & command.ToUpper Then
                            found = True
                            desiredline = i
                            Exit For
                        Else
                            found = False
                        End If
                    End If
                Next
                If found = True Then
                    Try
                        For a = 0 To array1.Length - 1
                            If array1(desiredline + a) = "" Then
                                TextBox2.AppendText(Environment.NewLine)
                                'Exit Sub
                                Exit For
                            Else
                                TextBox2.AppendText(array1(desiredline + a) & Environment.NewLine)
                            End If
                        Next
                    Catch ex As Exception

                    End Try
                Else
                End If
            Next
            If found = False Then
                TextBox2.Text = "Ничего не найдено!" & Environment.NewLine & Environment.NewLine
            End If
        End If
    End Sub
End Class
