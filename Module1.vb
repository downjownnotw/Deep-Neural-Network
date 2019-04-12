Module Module1
    Sub Main()
        Console.WriteLine("Algoritma DNN (Deep Neural Networks)")
        Console.WriteLine("Contoh: penentuan penerimaan pengajuan kredit sepeda motor baru berdasarkan kelompok data yang sudah ada")
        Console.WriteLine("Diasumsikan ada 8 data pelanggan yang sudah diketahui datanya, yaitu Pelanggan A,B,C,D,E,F,G,H")
        Console.WriteLine("Masing-masing pelanggan memiliki kriteria, yaitu umur, jenis kelamin, skor kepribadian, dan memiliki nilai hasil yaitu Diterima / Ditolak")

        Console.WriteLine("Diasumsikan 8 data tersebut adalah sebagai berikut:")
        Console.WriteLine("Pelanggan, Umur, Jenis Kelamin, Skor Kepribadian, Hasil")
        Console.WriteLine("Pelanggan A, 44, Laki-laki, 3.55, Diterima")
        Console.WriteLine("Pelanggan B, 52, Perempuan, 4.71, Diterima")
        Console.WriteLine("Pelanggan C, 60, Perempuan, 6.56, Ditolak")
        Console.WriteLine("Pelanggan D, 56, Laki-laki, 6.8 , Ditolak")
        Console.WriteLine("Pelanggan E, 51, Laki-laki, 6.94, Ditolak")
        Console.WriteLine("Pelanggan F, 46, Perempuan, 6.52, Ditolak")
        Console.WriteLine("Pelanggan G, 48, Laki-laki, 4.25, Diterima")
        Console.WriteLine("Pelanggan H, 58, Perempuan, 5.71, Diterima")
        Console.WriteLine("")

        Dim data(9)() As Double
        data(0) = New Double() {44, -1, 3.55, 1, 0}
        data(1) = New Double() {52, +1, 4.71, 1, 0}
        data(2) = New Double() {60, +1, 6.56, 0, 1}
        data(3) = New Double() {56, -1, 6.8, 0, 1}
        data(4) = New Double() {51, -1, 6.94, 0, 1}
        data(5) = New Double() {46, +1, 6.52, 0, 1}
        data(6) = New Double() {48, -1, 4.25, 1, 0}
        data(7) = New Double() {58, +1, 5.71, 1, 0}

        Console.WriteLine("Selanjutnya ada 2 orang pelanggan baru yang mengajukan kredit sepeda motor")
        Console.WriteLine("Maka tentukan pelanggan ini nantinya akan termasuk dalam kelompok Diterima / Ditolak")
        Console.WriteLine("Diasumsikan data awalnya adalah sebagai berikut:")

        Console.WriteLine("Pelanggan, Umur, Jenis Kelamin, Skor Kepribadian")
        Console.WriteLine("Pelanggan I, 47, Perempuan, 6.05")
        Console.WriteLine("Pelanggan J, 52, Laki-Laki, 5")
        Console.WriteLine("")

        data(8) = New Double() {47, +1, 6.05, -1, -1}
        data(9) = New Double() {52, -1, 5, -1, -1}

        Dim kolom() As Integer = {0, 2}
        Dim jumlahBaris As Integer = data.Length
        Dim jumlahKolom As Integer = data(0).Length

        Dim hasil(1)() As Double
        For i = 0 To 1
            hasil(i) = New Double(jumlahKolom - 1) {}
        Next i

        For c = 0 To jumlahKolom - 1
            Dim total As Double = 0.0
            For r = 0 To jumlahBaris - 1
                total += data(r)(c)
            Next r
            Dim rata2 As Double = total / jumlahBaris
            hasil(0)(c) = rata2

            Dim totalKuadrat As Double = 0.0
            For r = 0 To jumlahBaris - 1
                totalKuadrat += (data(r)(c) - rata2) * (data(r)(c) - rata2)
            Next r
            Dim stdDev As Double = Math.Sqrt(totalKuadrat / jumlahBaris)
            hasil(1)(c) = stdDev
        Next c

        For c = 0 To kolom.Length - 1
            Dim j As Integer = kolom(c)
            Dim rata2 As Double = hasil(0)(j)
            Dim stdDev As Double = hasil(1)(j)

            For i = 0 To jumlahBaris - 1
                data(i)(j) = (data(i)(j) - rata2) / stdDev
            Next i
        Next c

        Dim contohData(7)() As Double
        For i As Integer = 0 To 7
            contohData(i) = data(i)
        Next
        Dim dataBaru(1)() As Double
        For i As Integer = 8 To 9
            dataBaru(i - 8) = data(i)
        Next

        Const jumlahSarafInput As Integer = 3
        Const jumlahSarafTersembunyi1 As Integer = 4
        Const jumlahSarafTersembunyi2 As Integer = 5
        Const jumlahSarafOutput As Integer = 2
        Dim jst As New JaringanSaraf(jumlahSarafInput, jumlahSarafTersembunyi1, jumlahSarafTersembunyi2, jumlahSarafOutput)

        Const jumlahPartikel As Integer = 10

        Const jumlahIterasi As Integer = 500

        Console.WriteLine("jumlah partikel         = " & jumlahPartikel)
        Console.WriteLine("jumlah maksimal iterasi = " & jumlahIterasi)
        Console.WriteLine("")

        Dim bobotTerbaik() As Double = jst.PSO(contohData, jumlahPartikel, jumlahIterasi)
        jst.setBobot(bobotTerbaik)

        Console.WriteLine("Nilai bobot dan nilai bias terbaik yang ditemukan adalah:")
        For i As Integer = 0 To bobotTerbaik.Length - 1
            If i > 0 AndAlso i Mod 10 = 0 Then Console.WriteLine("")
            Console.Write(IIf(bobotTerbaik(i) >= 0.0, " ", "-") & Math.Abs(bobotTerbaik(i)).ToString("F3").PadLeft(6) & " ")
        Next
        Console.WriteLine(vbCrLf)

        Console.WriteLine("Pelanggan    Data Normalisasi   Hasil     Hasil Perhitungan       Benar/Salah?")
        Dim jumlahBenar As Integer = 0, jumlahSalah As Integer = 0
        For i As Integer = 0 To contohData.Length - 1
            Console.Write("Pelanggan " & Chr(i + 65) & "  ")

            Dim input(jumlahKolom - 3) As Double
            Array.Copy(contohData(i), input, jumlahKolom - 2)
            For j = 0 To input.Length - 1
                Console.Write(IIf(input(j) >= 0, " ", "") & input(j).ToString("F2") & " ")
            Next j
            Console.Write(" ")

            Console.Write(IIf(contohData(i)(jumlahKolom - 2) < contohData(i)(jumlahKolom - 1), "Ditolak ", "Diterima"))
            Console.Write("  ")

            Dim output() As Double = jst.hitungNilaiOutput(input)
            For j = 0 To output.Length - 1
                Console.Write(output(j).ToString("F2") & " ")
            Next j
            Console.Write(" ")

            If output(0) < output(1) Then
                Console.Write("-> Ditolak   ")
                If contohData(i)(jumlahKolom - 2) < contohData(i)(jumlahKolom - 1) Then
                    jumlahBenar += 1
                    Console.WriteLine("Benar")
                ElseIf contohData(i)(jumlahKolom - 2) > contohData(i)(jumlahKolom - 1) Then
                    jumlahSalah += 1
                    Console.WriteLine("Salah")
                End If
            Else
                Console.Write("-> Diterima  ")
                If contohData(i)(jumlahKolom - 2) < contohData(i)(jumlahKolom - 1) Then
                    jumlahSalah += 1
                    Console.WriteLine("Salah")
                ElseIf contohData(i)(jumlahKolom - 2) > contohData(i)(jumlahKolom - 1) Then
                    jumlahBenar += 1
                    Console.WriteLine("Benar")
                End If
            End If
        Next

        Console.WriteLine("Jumlah perhitungan benar = " & jumlahBenar & ", jumlah perhitungan salah = " & jumlahSalah)
        Console.WriteLine("Tingkat kecocokan perhitungan dengan hasil data adalah " & (jumlahBenar / (jumlahBenar + jumlahSalah)).ToString("F2"))
        Console.WriteLine("")

        Console.WriteLine("Pelanggan    Data Normalisasi   Hasil Perhitungan")
        For i As Integer = 0 To dataBaru.Length - 1
            Console.Write("Pelanggan " & Chr(i + 65 + 8) & "  ")

            Dim input(jumlahKolom - 3) As Double
            Array.Copy(dataBaru(i), input, jumlahKolom - 2)
            For j = 0 To input.Length - 1
                Console.Write(IIf(input(j) >= 0, " ", "") & input(j).ToString("F2") & " ")
            Next j
            Console.Write(" ")

            Dim output() As Double = jst.hitungNilaiOutput(input)
            For j = 0 To output.Length - 1
                Console.Write(output(j).ToString("F2") & " ")
            Next j
            Console.Write(" ")

            If output(0) < output(1) Then
                Console.Write("-> Ditolak ")
            Else
                Console.Write("-> Diterima")
            End If
            Console.WriteLine("")
        Next

        Console.ReadLine()
    End Sub
End Module

Public Class JaringanSaraf
    Private rnd As Random

    Private jumlahSarafInput As Integer
    Private jumlahSarafTersembunyi1 As Integer
    Private jumlahSarafTersembunyi2 As Integer
    Private jumlahSarafOutput As Integer

    Private inputs As Double()

    Private boboti1 As Double()()
    Private bias1 As Double()
    Private output1 As Double()

    Private bobot12 As Double()()
    Private bias2 As Double()
    Private output2 As Double()

    Private bobot2o As Double()()
    Private biaso As Double()

    Private outputs As Double()

    Public Sub New(jumlahSarafInput As Integer, jumlahSarafTersembunyi1 As Integer, jumlahSarafTersembunyi2 As Integer, jumlahSarafOutput As Integer)
        rnd = New Random(0)

        Me.jumlahSarafInput = jumlahSarafInput
        Me.jumlahSarafTersembunyi1 = jumlahSarafTersembunyi1
        Me.jumlahSarafTersembunyi2 = jumlahSarafTersembunyi2
        Me.jumlahSarafOutput = jumlahSarafOutput

        inputs = New Double(jumlahSarafInput - 1) {}

        Dim boboti1(jumlahSarafInput - 1)() As Double
        For i = 0 To jumlahSarafInput - 1
            boboti1(i) = New Double(jumlahSarafTersembunyi1 - 1) {}
        Next i
        Me.boboti1 = boboti1
        bias1 = New Double(jumlahSarafTersembunyi1 - 1) {}
        output1 = New Double(jumlahSarafTersembunyi1 - 1) {}

        Dim bobot12(jumlahSarafTersembunyi1 - 1)() As Double
        For i = 0 To jumlahSarafTersembunyi1 - 1
            bobot12(i) = New Double(jumlahSarafTersembunyi2 - 1) {}
        Next i
        Me.bobot12 = bobot12
        bias2 = New Double(jumlahSarafTersembunyi2 - 1) {}
        output2 = New Double(jumlahSarafTersembunyi2 - 1) {}

        Dim bobot2o(jumlahSarafTersembunyi2 - 1)() As Double
        For i = 0 To jumlahSarafTersembunyi2 - 1
            bobot2o(i) = New Double(jumlahSarafOutput - 1) {}
        Next i
        Me.bobot2o = bobot2o
        biaso = New Double(jumlahSarafOutput - 1) {}

        outputs = New Double(jumlahSarafOutput - 1) {}
        Dim jumlahBobot As Integer = (jumlahSarafInput * jumlahSarafTersembunyi1) + jumlahSarafTersembunyi1 + (jumlahSarafTersembunyi1 * jumlahSarafTersembunyi2) + jumlahSarafTersembunyi2 + (jumlahSarafTersembunyi2 * jumlahSarafOutput) + jumlahSarafOutput
        Dim bobot As Double() = New Double(jumlahBobot - 1) {}
        Dim lo As Double = 0.0001
        Dim hi As Double = 0.001
        For i As Integer = 0 To bobot.Length - 1
            bobot(i) = (hi - lo) * rnd.NextDouble() + lo
        Next
        Me.setBobot(bobot)
    End Sub
    Public Sub setBobot(bobot As Double())
        Dim jumlahBobot As Integer = (jumlahSarafInput * jumlahSarafTersembunyi1) + jumlahSarafTersembunyi1 + (jumlahSarafTersembunyi1 * jumlahSarafTersembunyi2) + jumlahSarafTersembunyi2 + (jumlahSarafTersembunyi2 * jumlahSarafOutput) + jumlahSarafOutput
        If bobot.Length <> jumlahBobot Then
            Throw New Exception("Pada fungsi setBobot, panjang matriks bobot: " & bobot.Length & " tidak sama dengan jumlah bobot yang seharusnya, yaitu " & jumlahBobot)
        End If

        Dim k As Integer = 0

        For i As Integer = 0 To jumlahSarafInput - 1
            For j As Integer = 0 To jumlahSarafTersembunyi1 - 1
                boboti1(i)(j) = bobot(k)
                k += 1
            Next
        Next

        For i As Integer = 0 To jumlahSarafTersembunyi1 - 1
            bias1(i) = bobot(k)
            k += 1
        Next

        For i As Integer = 0 To jumlahSarafTersembunyi1 - 1
            For j As Integer = 0 To jumlahSarafTersembunyi2 - 1
                bobot12(i)(j) = bobot(k)
                k += 1
            Next
        Next

        For i As Integer = 0 To jumlahSarafTersembunyi2 - 1
            bias2(i) = bobot(k)
            k += 1
        Next

        For i As Integer = 0 To jumlahSarafTersembunyi2 - 1
            For j As Integer = 0 To jumlahSarafOutput - 1
                bobot2o(i)(j) = bobot(k)
                k += 1
            Next
        Next

        For i As Integer = 0 To jumlahSarafOutput - 1
            biaso(i) = bobot(k)
            k += 1
        Next
    End Sub

    Public Function PSO(ByVal contohData()() As Double, jumlahPartikel As Integer, jumlahIterasi As Integer) As Double()
        Dim jumlahBobot As Integer = (jumlahSarafInput * jumlahSarafTersembunyi1) + jumlahSarafTersembunyi1 + (jumlahSarafTersembunyi1 * jumlahSarafTersembunyi2) + jumlahSarafTersembunyi2 + (jumlahSarafTersembunyi2 * jumlahSarafOutput) + jumlahSarafOutput
        Dim dimensi As Integer = jumlahBobot

        Console.WriteLine("Memasuki pencarian nilai bobot dan bias menggunakan algoritma PSO (Particle Swarm Optimization) sebanyak " & jumlahBobot & " buah")

        Dim minNilai As Double = -20.0
        Dim maksNilai As Double = 20.0

        Dim minKecepatan As Double = -0.1 * maksNilai
        Dim maksKecepatan As Double = 0.1 * maksNilai

        Dim swarm(jumlahPartikel - 1) As Partikel
        Dim posisiTerbaik(dimensi - 1) As Double
        Dim fitnessTerbaik As Double = Double.MaxValue


        For i = 0 To swarm.Length - 1
            Dim posisiAcak(dimensi - 1) As Double
            For j = 0 To posisiAcak.Length - 1
                Dim lo As Double = minNilai
                Dim hi As Double = maksNilai
                posisiAcak(j) = (hi - lo) * rnd.NextDouble + lo
            Next j

            Dim fitness As Double = CrossEntropy(contohData, posisiAcak)

            Dim kecepatanAcak(dimensi - 1) As Double
            For j = 0 To kecepatanAcak.Length - 1
                Dim lo As Double = -1.0 * Math.Abs(maksNilai - minNilai)
                Dim hi As Double = Math.Abs(maksNilai - minNilai)
                kecepatanAcak(j) = (hi - lo) * rnd.NextDouble + lo
            Next j

            swarm(i) = New Partikel(posisiAcak, fitness, kecepatanAcak, posisiAcak, fitness)

            If swarm(i).fitness < fitnessTerbaik Then
                fitnessTerbaik = swarm(i).fitness
                swarm(i).posisi.CopyTo(posisiTerbaik, 0)
            End If
        Next i

        Const w As Double = 0.729
        Const c1 As Double = 1.49445
        Const c2 As Double = 1.49445

        Dim r1, r2 As Double

        Dim iterasi As Integer = 0
        Do While iterasi < jumlahIterasi
            iterasi += 1

            Dim kecepatanBaru(dimensi - 1) As Double
            Dim posisiBaru(dimensi - 1) As Double
            Dim fitnessBaru As Double

            For i = 0 To swarm.Length - 1
                Dim partikelTerpilih As Partikel = swarm(i)

                For j = 0 To partikelTerpilih.kecepatan.Length - 1
                    r1 = rnd.NextDouble
                    r2 = rnd.NextDouble

                    kecepatanBaru(j) = (w * partikelTerpilih.kecepatan(j)) + (c1 * r1 * (partikelTerpilih.posisiTerbaik(j) - partikelTerpilih.posisi(j))) + (c2 * r2 * (posisiTerbaik(j) - partikelTerpilih.posisi(j)))

                    If kecepatanBaru(j) < minKecepatan Then
                        kecepatanBaru(j) = minKecepatan
                    ElseIf kecepatanBaru(j) > maksKecepatan Then
                        kecepatanBaru(j) = maksKecepatan
                    End If
                Next j

                kecepatanBaru.CopyTo(partikelTerpilih.kecepatan, 0)
                For j = 0 To partikelTerpilih.posisi.Length - 1
                    posisiBaru(j) = partikelTerpilih.posisi(j) + kecepatanBaru(j)

                    If posisiBaru(j) < minNilai Then
                        posisiBaru(j) = minNilai
                    ElseIf posisiBaru(j) > maksNilai Then
                        posisiBaru(j) = maksNilai
                    End If
                Next j

                posisiBaru.CopyTo(partikelTerpilih.posisi, 0)
                fitnessBaru = CrossEntropy(contohData, posisiBaru)
                partikelTerpilih.fitness = fitnessBaru

                If fitnessBaru < partikelTerpilih.fitnessTerbaik Then
                    posisiBaru.CopyTo(partikelTerpilih.posisiTerbaik, 0)
                    partikelTerpilih.fitnessTerbaik = fitnessBaru
                End If

                If fitnessBaru < fitnessTerbaik Then
                    posisiBaru.CopyTo(posisiTerbaik, 0)
                    fitnessTerbaik = fitnessBaru

                End If
            Next i
        Loop

        Return posisiTerbaik
    End Function

    Private Function CrossEntropy(ByVal contohData()() As Double, ByVal bobot() As Double) As Double
        Me.setBobot(bobot)

        Dim jumlahCrossEntropy As Double = 0.0

        For i = 0 To contohData.Length - 1
            Dim inputTerpilih(2) As Double
            Dim perkiraanOutput(1) As Double
            Array.Copy(contohData(i), inputTerpilih, 3)
            Array.Copy(contohData(i), 3, perkiraanOutput, 0, 2)

            Dim outputTerpilih() As Double = Me.hitungNilaiOutput(inputTerpilih)

            Dim jumlah As Double = 0.0
            For j = 0 To outputTerpilih.Length - 1
                If perkiraanOutput(j) <> 0.0 Then
                    jumlah += perkiraanOutput(j) * Math.Log(outputTerpilih(j))
                End If
            Next j
            jumlahCrossEntropy += jumlah
        Next i
        Return -jumlahCrossEntropy
    End Function
    Public Function hitungNilaiOutput(input As Double()) As Double()
        If input.Length <> jumlahSarafInput Then
            Throw New Exception("Pada fungsi hitungNilaiOutput, panjang array input " & inputs.Length & " tidak sama dengan jumlah saraf input yang seharusnya, yaitu " & jumlahSarafInput)
        End If

        Dim JumlahBobotDanBias1 As Double() = New Double(jumlahSarafTersembunyi1 - 1) {}
        Dim JumlahBobotDanBias2 As Double() = New Double(jumlahSarafTersembunyi2 - 1) {}
        Dim JumlahBobotDanBiaso As Double() = New Double(jumlahSarafOutput - 1) {}

        For i As Integer = 0 To input.Length - 1
            Me.inputs(i) = input(i)
        Next

        For j As Integer = 0 To jumlahSarafTersembunyi1 - 1
            For i As Integer = 0 To jumlahSarafInput - 1
                JumlahBobotDanBias1(j) += Me.inputs(i) * Me.boboti1(i)(j)
            Next
        Next

        For i As Integer = 0 To jumlahSarafTersembunyi1 - 1
            JumlahBobotDanBias1(i) += Me.bias1(i)
        Next

        For i As Integer = 0 To jumlahSarafTersembunyi1 - 1
            Me.output1(i) = HyperTan(JumlahBobotDanBias1(i))
        Next

        For j As Integer = 0 To jumlahSarafTersembunyi2 - 1
            For i As Integer = 0 To jumlahSarafTersembunyi1 - 1
                JumlahBobotDanBias2(j) += output1(i) * Me.bobot12(i)(j)
            Next
        Next

        For i As Integer = 0 To jumlahSarafTersembunyi2 - 1
            JumlahBobotDanBias2(i) += Me.bias2(i)
        Next

        For i As Integer = 0 To jumlahSarafTersembunyi2 - 1
            Me.output2(i) = HyperTan(JumlahBobotDanBias2(i))
        Next

        For j As Integer = 0 To jumlahSarafOutput - 1
            For i As Integer = 0 To jumlahSarafTersembunyi2 - 1
                JumlahBobotDanBiaso(j) += output2(i) * bobot2o(i)(j)
            Next
        Next

        For i As Integer = 0 To jumlahSarafOutput - 1
            JumlahBobotDanBiaso(i) += biaso(i)
        Next

        Dim hasilSoftMax As Double() = Softmax(JumlahBobotDanBiaso)
        Array.Copy(hasilSoftMax, outputs, hasilSoftMax.Length)

        Dim hasil As Double() = New Double(jumlahSarafOutput - 1) {}
        Array.Copy(Me.outputs, hasil, hasil.Length)
        Return hasil
    End Function

    Private Shared Function HyperTan(x As Double) As Double
        If x < -20.0 Then
            Return -1.0
        ElseIf x > 20.0 Then
            Return 1.0
        Else
            Return Math.Tanh(x)
        End If
    End Function
    Private Shared Function Softmax(JumlahBobotDanBiaso As Double()) As Double()
        Dim maksData As Double = JumlahBobotDanBiaso(0)
        For i = 0 To JumlahBobotDanBiaso.Length - 1
            If JumlahBobotDanBiaso(i) > maksData Then
                maksData = JumlahBobotDanBiaso(i)
            End If
        Next i

        Dim skala As Double = 0.0
        For i = 0 To JumlahBobotDanBiaso.Length - 1
            skala += Math.Exp(JumlahBobotDanBiaso(i) - maksData)
        Next i

        Dim hasil(JumlahBobotDanBiaso.Length - 1) As Double
        For i = 0 To JumlahBobotDanBiaso.Length - 1
            hasil(i) = Math.Exp(JumlahBobotDanBiaso(i) - maksData) / skala
        Next i

        Return hasil
    End Function
End Class

Public Class Partikel
    Public posisi() As Double
    Public fitness As Double
    Public kecepatan() As Double

    Public posisiTerbaik() As Double
    Public fitnessTerbaik As Double

    Public Sub New(ByVal posisi() As Double, ByVal fitness As Double, ByVal kecepatan() As Double, ByVal posisiTerbaik() As Double, ByVal fitnessTerbaik As Double)
        Me.posisi = New Double(posisi.Length - 1) {}
        posisi.CopyTo(Me.posisi, 0)
        Me.fitness = fitness
        Me.kecepatan = New Double(kecepatan.Length - 1) {}
        kecepatan.CopyTo(Me.kecepatan, 0)
        Me.posisiTerbaik = New Double(posisiTerbaik.Length - 1) {}
        posisiTerbaik.CopyTo(Me.posisiTerbaik, 0)
        Me.fitnessTerbaik = fitnessTerbaik
    End Sub
End Class
