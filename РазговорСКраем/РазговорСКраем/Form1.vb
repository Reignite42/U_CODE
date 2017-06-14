Imports System.IO
Imports System.Threading
Public Class Form1

    Dim IncomingPrompt As String
    Dim IncomingPromptLength As String
    Dim RandomizeAnswer As Random
    Dim PhraseSynonym As String
    Dim firstPhrase As String
    Dim addPhrase As String

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ListBox1.Items.Clear()
        Send(TextBox2.Text)
        IncomingPrompt = TextBox2.Text

        TextBox2.Text = ""

        If EDGEState = "Answer" Then
            Answering()
            Exit Sub
        End If
        
        If EDGEState = "Learn" Then
            If IncomingPrompt.ToLower = "*всё" Then
                LearnedPrompt = False
                EDGEState = "Answer"
                EDGEAnswer("Вот и отлично.")
                Exit Sub
                State.Start()
            Else
                If LearnedPrompt = False Then
                    LearnedPrompt = True
                    PromptForLearn = PromptForLearn.Replace(".", "")
                    PromptForLearn = PromptForLearn.Replace("!", "")
                    File.AppendAllText(Application.StartupPath & "\EDGE\Answers\Answers.txt", "*" & PromptForLearn.ToLower & "*" & IncomingPrompt & Environment.NewLine)
                    Learning()
                Else
                    PromptForLearn = PromptForLearn.Replace(".", "")
                    PromptForLearn = PromptForLearn.Replace("!", "")
                    File.AppendAllText(Application.StartupPath & "\EDGE\Answers\Answers.txt", "*" & PromptForLearn.ToLower & "*" & IncomingPrompt & Environment.NewLine)
                    Learning()
                End If
            End If
        End If

        If EDGEState = "Synonym" Then
            If IncomingPrompt.ToLower = "*всё" Then
                PhraseChoosen = False
                SynonymicalPrompt = False
                EDGEState = "Answer"
                EDGEAnswer("Вот и отлично.")
                Exit Sub
                State.Start()
            Else
                If PhraseChoosen = False Then
                    'MsgBox(IncomingPrompt)
                    IncomingPrompt = IncomingPrompt.Replace(".", "")
                    IncomingPrompt = IncomingPrompt.Replace("!", "")
                    IncomingPrompt = IncomingPrompt.Replace("?", "")
                    IncomingPromptLength = IncomingPrompt.Length
                    PhraseChoosen = True
                    Dim found As Boolean
                    Dim Answers() As String = File.ReadAllLines(Application.StartupPath & "\EDGE\Answers\Answers.txt")
                    For a = 0 To Answers.Length - 1
                        If Answers(a).ToLower.Contains("*" & IncomingPrompt.ToLower & "*") Then
                            found = True
                            ListBox1.Items.Add(Answers(a).Remove(0, IncomingPromptLength + 2))
                        End If
                    Next
                    If found = False Then


                    Else
                        SynonymLearning()
                    End If
                Else
                    'MsgBox("LOL")
                    IncomingPrompt = IncomingPrompt.Replace(".", "")
                    IncomingPrompt = IncomingPrompt.Replace("!", "")
                    IncomingPrompt = IncomingPrompt.Replace("?", "")
                    PhraseSynonym = IncomingPrompt
                    For a = 0 To ListBox1.Items.Count - 1
                        File.AppendAllText(Application.StartupPath & "\EDGE\Answers\Answers.txt", "*" & PhraseSynonym.ToLower & "*" & ListBox1.Items.Item(a) & Environment.NewLine)
                    Next
                    SynonymicalPrompt = True
                    SynonymLearning()
                End If
            End If
        End If

        If EDGEState = "AddingPhrases" Then
            If IncomingPrompt.ToLower = "*всё" Then
                EDGEState = "Answer"
                EDGEAnswer("Вот и отлично.")
                Exit Sub
                State.Start()
            Else
                If AddPhraseChoosen = False Then
                    If FirstPhraseChoosen = True Then
                        addPhrase = IncomingPrompt
                        AddPhraseChoosen = True
                        File.AppendAllText(Application.StartupPath & "\EDGE\Answers\Answers.txt", "*" & firstPhrase.ToLower & "*" & addPhrase & Environment.NewLine)
                        AddingPhrases()
                        Exit Sub
                    End If
                    IncomingPrompt = IncomingPrompt.Replace(".", "")
                    IncomingPrompt = IncomingPrompt.Replace("!", "")
                    IncomingPrompt = IncomingPrompt.Replace("?", "")
                    IncomingPromptLength = IncomingPrompt.Length
                    firstPhrase = IncomingPrompt.ToLower
                    Dim found As Boolean
                    Dim Answers() As String = File.ReadAllLines(Application.StartupPath & "\EDGE\Answers\Answers.txt")
                    For a = 0 To Answers.Length - 1
                        If Answers(a).ToLower.Contains("*" & IncomingPrompt.ToLower & "*") Then
                            found = True
                            ListBox1.Items.Add(Answers(a).Remove(0, IncomingPromptLength + 2))
                        End If
                    Next
                    If found = False Then


                    Else
                        FirstPhraseChoosen = True
                        AddingPhrases()
                    End If
                Else
                    addPhrase = IncomingPrompt
                    File.AppendAllText(Application.StartupPath & "\EDGE\Answers\Answers.txt", "*" & firstPhrase.ToLower & "*" & addPhrase & Environment.NewLine)
                    AddingPhrases()
                End If
            End If
        End If
    End Sub

    Private Sub Send(ByVal Text As String)
        TextBox1.ForeColor = Color.DarkGreen
        TextBox1.AppendText(Text & Environment.NewLine)
    End Sub

    Private Sub EDGEAnswer(ByVal Text As String)
        TextBox3.ForeColor = Color.Maroon
        TextBox3.AppendText(Text & Environment.NewLine)
    End Sub

    Dim PromptForLearn As String

    Private Sub Answering()
        Dim Found As Boolean
        IncomingPrompt = IncomingPrompt.Replace(".", "")
        IncomingPrompt = IncomingPrompt.Replace("!", "")
        IncomingPrompt = IncomingPrompt.Replace("?", "")
        IncomingPrompt = IncomingPrompt.ToLower
        IncomingPromptLength = IncomingPrompt.Length
        If IncomingPrompt.Contains("запусти") Or IncomingPrompt.Contains("запуск") Then
            shell1(IncomingPrompt)
            Exit Sub
        ElseIf IncomingPrompt.ToLower.Contains("что значит") Or IncomingPrompt.ToLower.Contains("что такое") Then
            search(IncomingPrompt)
            Exit Sub
        End If
        Dim Answers() As String = File.ReadAllLines(Application.StartupPath & "\EDGE\Answers\Answers.txt")
        For a = 0 To Answers.Length - 1
            If Answers(a).ToLower.Contains("*" & IncomingPrompt.ToLower & "*") Then
                Found = True
                ListBox1.Items.Add(Answers(a).Remove(0, IncomingPromptLength + 2))
                If Answers(a).Contains("<Music>") Then
                    DoCommand("Music")
                    Exit Sub
                ElseIf Answers(a).Contains("<ESrch>") Then
                    DoCommand("Search")
                    Exit Sub
                ElseIf Answers(a).Contains("<StrtP>") Then
                    DoCommand("StartProcess")
                    Exit Sub
                ElseIf Answers(a).Contains("<ShutD>") Then
                    DoCommand("ShuttingDown")
                    Exit Sub
                End If
            End If
        Next
        If Found = True Then
            RandomizeAnswer = New Random
            Dim NumberOfAnswer As Integer
            NumberOfAnswer = RandomizeAnswer.Next(0, ListBox1.Items.Count - 1)
            EDGEAnswer(ListBox1.Items(NumberOfAnswer))
        Else
            PromptForLearn = IncomingPrompt
            EDGEState = "Learn"
            Learning()
            State.Stop()
            Exit Sub
        End If
    End Sub

    Private Sub TextBox2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1.PerformClick()
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If File.Exists(Application.StartupPath & "\EDGE\Answers\Answers.txt") = False Then
            File.Create(Application.StartupPath & "\EDGE\Answers\Answers.txt", 5 * 1024 * 1024, FileOptions.Asynchronous)
        End If
    End Sub

    Dim EDGEState As String = "Answer"
    Dim LearnedPrompt As Boolean = False

    Private Sub Learning()
        If LearnedPrompt = False Then
            EDGEAnswer("Как мне ответить?")
        End If
        If LearnedPrompt = True Then
            EDGEAnswer("Как мне ещё ответить?")
        End If
    End Sub

    Dim SynonymicalPrompt As Boolean
    Dim PhraseChoosen As Boolean = False

    Private Sub SynonymLearning()
        If SynonymicalPrompt = False Then
            If PhraseChoosen = True Then
                EDGEAnswer("Какой для этой фразы синоним?")
            Else
                EDGEAnswer("Введи фразу.")
            End If
        Else
            EDGEAnswer("Что-то ещё?")
        End If
    End Sub

    Dim FirstPhraseChoosen As Boolean
    Dim AddPhraseChoosen As Boolean

    Private Sub AddingPhrases()
        If AddPhraseChoosen = False Then
            If FirstPhraseChoosen = False Then
                EDGEAnswer("Введи первую фразу.")
            Else
                EDGEAnswer("Введи фразу ответа.")
            End If
        Else
            EDGEAnswer("Что-то ещё?.")
        End If
    End Sub

    Private Sub State_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles State.Tick
        If EDGEState = "Learn" Then
            LearnedPrompt = False
            Learning()
            State.Stop()
        End If
        If EDGEState = "Synonym" Then
            SynonymLearning()
            State.Stop()
        End If
        If EDGEState = "AddingPhrases" Then
            AddingPhrases()
            State.Stop()
        End If
    End Sub

    Private Sub DoCommand(ByVal Type As String)
        Select Case Type
            Case "Music"
                Dim Music() As String
                Dim randMusic As Random
                Dim numberOfMusic
                randMusic = New Random
                Music = Directory.GetFiles(Application.StartupPath & "\Edge\Music\")

                EDGEAnswer("Запускаю музыку")
                Try
                    If Music.Length > 0 Then
                        numberOfMusic = randMusic.Next(0, Music.Length - 1)
                        Process.Start(Music(numberOfMusic))
                    Else
                        EDGEAnswer("Не могу найти музыку. Если что, добавь её в папку \\EDGE\Music\")
                    End If
                Catch ex As Exception
                    If Music.Length = 0 Then
                        EDGEAnswer("Не могу найти музыку. Если что, добавь её в папку \\EDGE\Music\")
                    Else
                        EDGEAnswer(ex.Message)
                    End If
                End Try
            Case "Search"
                search(IncomingPrompt)
                EDGEAnswer("Ищу")
            Case "StartProcess"
                shell(IncomingPrompt)
                EDGEAnswer("Запускаю процесс")
            Case "ShuttingDown"
                EDGEAnswer("Выключаю компьютер")
        End Select
    End Sub

    Private Sub shell1(ByVal process As String)
        process = process.Replace("запусти", "")
        process = process.Replace("запуск", "")
        process = process.Replace(" ", "")
        EDGEAnswer("Запускаю " & process.ToUpper)
        Try
            shell(process)
        Catch ex As Exception
            Try
                Shell(process & ".exe")
            Catch ex1 As Exception
                EDGEAnswer(ex1.Message)
            End Try
        End Try

    End Sub

    Private Sub search(ByVal command As String)
        Dim desiredline As Integer
        Dim found As Boolean
        Dim commandLength As Integer
        Dim tempString As String
        Dim dictionaries As String()
        dictionaries = Directory.GetFiles(Application.StartupPath & "\Edge\Dictionaries\")
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
                                EDGEAnswer("")
                                'Exit Sub
                                Exit For
                            Else
                                EDGEAnswer(array1(desiredline + a))
                            End If
                        Next
                    Catch ex As Exception

                    End Try
                Else
                End If
            Next
            If found = False Then
                EDGEAnswer("Ничего не найдено!")
            End If
        Else
            Exit Sub
        End If
    End Sub
End Class
