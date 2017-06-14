Imports System.IO
Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim line As String

        Dim array1() As String
        array1 = File.ReadAllLines("D:\Documents and Settings\User\Рабочий стол\MInd\Mind\Mind\bin\Debug\Dictionaries\Словарь Даля.txt", System.Text.Encoding.Default)
        For i = 0 To array1.Length - 1

            line = array1(i)
            If line.Length < 72 Then
                File.AppendAllText("D:\Documents and Settings\User\Рабочий стол\MInd\Mind\Mind\bin\Debug\Dictionaries\Словарь Даля(Full1).txt", line & Environment.NewLine & Environment.NewLine, System.Text.Encoding.Default)
            Else
                If line.StartsWith("   ") Then
                    File.AppendAllText("D:\Documents and Settings\User\Рабочий стол\MInd\Mind\Mind\bin\Debug\Dictionaries\Словарь Даля(Full1).txt", "  " & line & Environment.NewLine, System.Text.Encoding.Default)
                Else
                    File.AppendAllText("D:\Documents and Settings\User\Рабочий стол\MInd\Mind\Mind\bin\Debug\Dictionaries\Словарь Даля(Full1).txt", line & Environment.NewLine, System.Text.Encoding.Default)
                End If
            End If
        Next
    End Sub
End Class
