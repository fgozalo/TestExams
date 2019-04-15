Imports System.IO
Imports System.Net

Public Class Questions
    Public good As Integer
    Public numero As Integer
    Public total_good As Integer
    Public total_bad As Integer
    Public total As Integer
    Public max As Integer
    Public min As Integer
    Public file_name As String
    Public folder_name As String
    Public tot_tiempo As Long
    Public tot_pregunta As Integer
    Public in_pregunta As Boolean
    Public in_exam As Boolean
    Public siguiente As Boolean



    Public Function GetRandom(ByVal Min As Integer, ByVal Max As Integer) As Integer
        Dim Generator As System.Random = New System.Random(Environment.TickCount)
        Generator.Next(Min, Max)
        Return Generator.Next(Min, Max)
    End Function

    Private Sub init_fields()
        RadioButton1.Text = "Empty"
        RadioButton2.Text = "Empty"
        RadioButton3.Text = "Empty"
        RadioButton4.Text = "Empty"
        TextBox1.Text = "Empty"
        TextBox1.Visible = False
        TextBox3.Visible = False
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        RadioButton3.Checked = False
        RadioButton4.Checked = False

        RadioButton1.BackColor = Color.LightGray
        RadioButton2.BackColor = Color.LightGray
        RadioButton3.BackColor = Color.LightGray
        RadioButton4.BackColor = Color.LightGray

        in_exam = True
        in_pregunta = True
        tot_pregunta = 0


    End Sub

    Private Sub guarda(donde As String)
        ' Guarda file_name en buenas o malas (TextBox4.Text  + donde)

        Dim fs As FileStream = Nothing

        If (Not File.Exists(TextBox4.Text + donde)) Then
            Try
                Using sw As New StreamWriter(File.Open(TextBox4.Text + donde, FileMode.OpenOrCreate))
                    sw.WriteLine(file_name)
                End Using
            Catch ex As Exception
                MsgBox("Error Creating Log File:" + TextBox4.Text + donde)
            End Try
        Else
            Try

                Using sw As New StreamWriter(File.Open(TextBox4.Text + donde, FileMode.Append))
                    sw.WriteLine(file_name)
                    sw.Close()
                End Using
            Catch ex As Exception
                MsgBox("Error Adding data Log File " + TextBox4.Text + donde)
            End Try

        End If


    End Sub


    Private Sub pregunta(id As Integer)

        init_fields()

        file_name = id.ToString() + ".htm.html"
        good = 0

        TextBox11.Text = file_name

        Dim objReader As New System.IO.StreamReader(TextBox4.Text + file_name)
        Dim str As String

        Try
            str = objReader.ReadLine
            str = objReader.ReadLine

            ' Question
            TextBox2.Text = str
            str = objReader.ReadLine
            If (str.ToString().StartsWith(TextBox9.Text)) Then
                good = 1
                str = Mid(str, Len(TextBox9.Text) + 1, Len(str) - Len(TextBox9.Text))
            End If
            ' Option A
            RadioButton1.Text = str

            str = objReader.ReadLine
            If (str.ToString().StartsWith(TextBox9.Text)) Then
                good = 2
                str = Mid(str, Len(TextBox9.Text) + 1, Len(str) - Len(TextBox9.Text))
            End If
            ' Option B
            RadioButton2.Text = str

            str = objReader.ReadLine
            If (str.ToString().StartsWith(TextBox9.Text)) Then
                good = 3
                str = Mid(str, Len(TextBox9.Text) + 1, Len(str) - Len(TextBox9.Text))
            End If
            ' Option C
            RadioButton3.Text = str

            str = objReader.ReadLine
            If (str.ToString().StartsWith(TextBox9.Text)) Then
                good = 4
                str = Mid(str, Len(TextBox9.Text) + 1, Len(str) - Len(TextBox9.Text))
            End If

            ' Option D            
            RadioButton4.Text = str
            str = objReader.ReadLine
            str = objReader.ReadLine

            str = WebUtility.UrlDecode(str)
            ' TextBox1.Visible = True
            TextBox1.Text = str


            If good = 0 Then MessageBox.Show("Error, No solution found!")

        Catch ex As Exception

        End Try

        objReader.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        siguiente = False
        numero = GetRandom(min, max)
        pregunta(numero)

    End Sub
    Private Sub click_radio(id As Integer)
        in_pregunta = False


        If id = 1 Then RadioButton1.BackColor = Color.Tomato
        If id = 2 Then RadioButton2.BackColor = Color.Tomato
        If id = 3 Then RadioButton3.BackColor = Color.Tomato
        If id = 4 Then RadioButton4.BackColor = Color.Tomato




        If good = 1 Then RadioButton1.BackColor = Color.LightGreen
        If good = 2 Then RadioButton2.BackColor = Color.LightGreen
        If good = 3 Then RadioButton3.BackColor = Color.LightGreen
        If good = 4 Then RadioButton4.BackColor = Color.LightGreen


        If TextBox3.Visible = False Then
            Dim resp(4) As String
            resp(1) = "A"
            resp(2) = "B"
            resp(3) = "C"
            resp(4) = "D"

            If good = id Then
                TextBox3.Text = "Correct!"
                total_good += 1
                guarda("buenas.txt")
            Else
                TextBox3.Text = "No!"
                total_bad += 1
                'TextBox1.Text = " La buena era la: " + resp(good) + vbCrLf + TextBox1.Text
                guarda("malas.txt")

            End If
            total += 1
            TextBox1.Visible = True
            TextBox3.Visible = True
            TextBox6.Text = total_good.ToString
            TextBox7.Text = total_bad.ToString
            TextBox8.Text = total.ToString
            Try
                TextBox10.Text = (total_good / total) * 100
            Catch ex As Exception

            End Try

        End If

    End Sub

    Private Sub RadioButton1_Click(sender As Object, e As EventArgs) Handles RadioButton1.Click
        click_radio(1)
    End Sub

    Private Sub RadioButton2_Click(sender As Object, e As EventArgs) Handles RadioButton2.Click
        click_radio(2)
    End Sub


    Private Sub RadioButton3_Click(sender As Object, e As EventArgs) Handles RadioButton3.Click
        click_radio(3)
    End Sub


    Private Sub RadioButton4_Click(sender As Object, e As EventArgs) Handles RadioButton4.Click
        click_radio(4)
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        siguiente = False
        numero = numero - 1
        If numero < min Then numero = max
        pregunta(numero)

    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        Try
            numero = Integer.Parse(TextBox5.Text.ToString())
        Catch ex As Exception

        End Try


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        siguiente = True

        Do

            numero = numero + 1
            If numero > max Then numero = min
            pregunta(numero)
            If TextBox14.Text <> "" Then
                If InStr(LCase(TextBox2.Text), LCase(TextBox14.Text), vbTextCompare) > 0 Then
                    'MessageBox.Show(TextBox2.Text + " contiene " + TextBox14.Text)
                    siguiente = False
                End If
                If InStr(LCase(RadioButton1.Text), LCase(TextBox14.Text), vbTextCompare) > 0 Then
                    'MessageBox.Show(RadioButton1.Text + " contiene " + TextBox14.Text)
                    siguiente = False
                End If
                If InStr(LCase(RadioButton2.Text), LCase(TextBox14.Text), vbTextCompare) > 0 Then
                    'MessageBox.Show(RadioButton2.Text + " contiene " + TextBox14.Text)
                    siguiente = False
                End If
                If InStr(LCase(RadioButton3.Text), LCase(TextBox14.Text), vbTextCompare) > 0 Then
                    'MessageBox.Show(RadioButton3.Text + " contiene " + TextBox14.Text)
                    siguiente = False
                End If
                If InStr(LCase(RadioButton4.Text), LCase(TextBox14.Text), vbTextCompare) > 0 Then
                    'MessageBox.Show(RadioButton4.Text + " contiene " + TextBox14.Text)
                    siguiente = False
                End If
            Else
                siguiente = False
            End If

            If CheckBox1.Checked Then
                If (FindStringInFile(TextBox4.Text + "malas.txt", numero.ToString() + ".htm.html")) Then siguiente = False Else siguiente = True
            End If

            Application.DoEvents()
        Loop While siguiente = True

    End Sub

    Public Function FindStringInFile(ByVal Filename As String, ByVal SearchString As String) As Boolean
        Dim Reader As System.IO.StreamReader
        Reader = New IO.StreamReader(Filename)
        Dim stringReader As String
        Try
            While Reader.Peek <> -1
                stringReader = Reader.ReadLine()
                If InStr(stringReader, SearchString) > 0 Then Return True
            End While
            Reader.Close()
            Return False
        Catch ex As Exception
            MessageBox.Show("Exception: " & ex.Message)
            Return False
        End Try
    End Function


    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged

    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        good = 0

        min = 1   ' CISM
        max = 726 ' CISM
        TextBox9.Text = "@"

        'min = 0   ' AWS-SAA
        'max = 207 ' AWS-SAA
        'TextBox9.Text = "***"
        folder_name = "C:\cism\out\"
        TextBox4.Text = folder_name
        'folder_NAME = "C:\cygwin64\home\laptop101\aws-saa\out\"
        init_fields()

    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If in_exam Then tot_tiempo = tot_tiempo + 1
        If in_pregunta Then tot_pregunta = tot_pregunta + 1
        Dim time As TimeSpan
        time = TimeSpan.FromSeconds(tot_tiempo)
        TextBox12.Text = time.ToString("hh\:mm\:ss")
        time = TimeSpan.FromSeconds(tot_pregunta)
        TextBox13.Text = time.ToString("mm\:ss")
    End Sub
End Class
