Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes : Imports clsOtrasFunciones

Public Class clsOrdenModelo
    Inherits clsADO

#Region "CAMPOS"

    Private mORDRE As Double
    Public Property ORDRE() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mORDRE = nzn(dvForm(PA).Row("ORDRE"), 0)
            Catch ex As Exception : End Try
            Return nzn(mORDRE, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            mORDRE = nzn(Value, 0)
            dvForm(PA).Row("ORDRE") = nzn(Value, 0) : guardarDV()
        End Set
    End Property

    Private mTEMPORADA As String
    Public Property TEMPORADA() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTEMPORADA = general.nz(dvForm(PA).Row("TEMPORADA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTEMPORADA, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mTEMPORADA = general.nz(Value, "")
            dvForm(PA).Row("TEMPORADA") = general.nz(Value, "") : guardarDV()
        End Set
    End Property

    Private mEXPEDICIO As Double
    Public Property EXPEDICIO() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mEXPEDICIO = nzn(dvForm(PA).Row("EXPEDICIO"), 0)
            Catch ex As Exception : End Try
            Return nzn(mEXPEDICIO, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            mEXPEDICIO = nzn(Value, 0)
            dvForm(PA).Row("EXPEDICIO") = nzn(Value, 0) : guardarDV()
        End Set
    End Property

    Private mENTRADA As Date
    Public Property ENTRADA() As Date
        Get
            If PA() = -1 Then Exit Property
            Try
                mENTRADA = dvForm(PA).Row("ENTRADA")
            Catch ex As Exception : End Try
            Return mENTRADA
        End Get
        Set(ByVal Value As Date)
            If PA() = -1 Then Exit Property
            mENTRADA = Value
            dvForm(PA).Row("ENTRADA") = Value : guardarDV()
        End Set
    End Property

    Private mPREVISTA As Date
    Public Property PREVISTA() As Date
        Get
            If PA() = -1 Then Exit Property
            Try
                mPREVISTA = dvForm(PA).Row("PREVISTA")
            Catch ex As Exception : End Try
            Return mPREVISTA
        End Get
        Set(ByVal Value As Date)
            If PA() = -1 Then Exit Property
            mPREVISTA = Value
            dvForm(PA).Row("PREVISTA") = Value : guardarDV()
        End Set
    End Property

    Private mFINAL As Date
    Public Property FINAL() As Date
        Get
            If PA() = -1 Then Exit Property
            Try
                mFINAL = dvForm(PA).Row("FINAL")
            Catch ex As Exception : End Try
            Return mFINAL
        End Get
        Set(ByVal Value As Date)
            If PA() = -1 Then Exit Property
            mFINAL = Value
            dvForm(PA).Row("FINAL") = Value : guardarDV()
        End Set
    End Property

    Private mMODEL As String
    Public Property MODEL() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mMODEL = general.nz(dvForm(PA).Row("MODEL"), "")
            Catch ex As Exception : End Try
            Return general.nz(mMODEL, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mMODEL = general.nz(Value, "")
            dvForm(PA).Row("MODEL") = general.nz(Value, "") : guardarDV()
        End Set
    End Property

    Private mCLIENT As Integer
    Public Property CLIENT() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mCLIENT = nzn(dvForm(PA).Row("CLIENT"), 0)
            Catch ex As Exception : End Try
            Return nzn(mCLIENT, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If esCodigoExistente(dtClients, CCClients, Value) Then
                If nzn(Value, 0) <> 0 Then
                    mCLIENT = nzn(Value, 0)
                    dvForm(PA).Row("NOMCLIENT") = general.OBN(Value, dtClients, CNClients)
                    dvForm(PA).Row("CLIENT") = nzn(Value, 0) : guardarDV()
                End If
            Else
                dvForm(PA).Row("CLIENT") = 0

                dvForm(PA).Row("NOMCLIENT") = "" : guardarDV()
                MessageBox.Show(rm.GetString("NOEXISTECLIENTS"))
            End If
        End Set
    End Property

    Private mNOMCLIENT As String
    Public Property NOMCLIENT() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNOMCLIENT = general.nz(dvForm(PA).Row("NOMCLIENT"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMCLIENT, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mNOMCLIENT = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("NOMCLIENT") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("NOMCLIENT") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mSERIE As String
    Public Property SERIE() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mSERIE = general.nz(dvForm(PA).Row("SERIE"), "")
            Catch ex As Exception : End Try
            Return general.nz(mSERIE, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mSERIE = general.nz(Value, "")
            dvForm(PA).Row("SERIE") = general.nz(Value, "") : guardarDV()
        End Set
    End Property

    Private mALBARA As String
    Public Property ALBARA() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mALBARA = general.nz(dvForm(PA).Row("ALBARA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mALBARA, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mALBARA = general.nz(Value, "")
            dvForm(PA).Row("ALBARA") = general.nz(Value, "") : guardarDV()
        End Set
    End Property

    Private mOBSERV As String
    Public Property OBSERV() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mOBSERV = general.nz(dvForm(PA).Row("OBSERV"), "")
            Catch ex As Exception : End Try
            Return general.nz(mOBSERV, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mOBSERV = general.nz(Value, "")
            dvForm(PA).Row("OBSERV") = general.nz(Value, "") : guardarDV()
        End Set
    End Property

    Private mPREU As Double
    Public Property PREU() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mPREU = nzn(dvForm(PA).Row("PREU"), 0)
            Catch ex As Exception : End Try
            Return nzn(mPREU, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            mPREU = nzn(Value, 0)
            dvForm(PA).Row("PREU") = nzn(Value, 0) : guardarDV()
        End Set
    End Property

    Private mFET As Boolean
    Public Property FET() As Boolean
        Get
            If PA() = -1 Then Exit Property
            Try
                mFET = dvForm(PA).Row("FET")
            Catch ex As Exception : End Try
            Return mFET
        End Get
        Set(ByVal Value As Boolean)
            If PA() = -1 Then Exit Property
            mFET = Value
            dvForm(PA).Row("FET") = Value : guardarDV()
        End Set
    End Property

    Private mALB As Boolean
    Public Property ALB() As Boolean
        Get
            If PA() = -1 Then Exit Property
            Try
                mALB = dvForm(PA).Row("ALB")
            Catch ex As Exception : End Try
            Return mALB
        End Get
        Set(ByVal Value As Boolean)
            If PA() = -1 Then Exit Property
            mALB = Value
            dvForm(PA).Row("ALB") = Value : guardarDV()
        End Set
    End Property

    Private mMOS As String
    Public Property MOS() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mMOS = general.nz(dvForm(PA).Row("MOS"), "")
            Catch ex As Exception : End Try
            Return general.nz(mMOS, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mMOS = general.nz(Value, "")
            dvForm(PA).Row("MOS") = general.nz(Value, "") : guardarDV()
        End Set
    End Property

    Private mTALLA01 As String
    Public Property TALLA01() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA01 = general.nz(dvForm(PA).Row("TALLA01"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLA01, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mTALLA01 = general.nz(Value, "")
            dvForm(PA).Row("TALLA01") = general.nz(Value, "") : guardarDV()
        End Set
    End Property

    Private mTALLA02 As String
    Public Property TALLA02() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA02 = general.nz(dvForm(PA).Row("TALLA02"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLA02, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mTALLA02 = general.nz(Value, "")
            dvForm(PA).Row("TALLA02") = general.nz(Value, "") : guardarDV()
        End Set
    End Property

    Private mTALLA03 As String
    Public Property TALLA03() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA03 = general.nz(dvForm(PA).Row("TALLA03"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLA03, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mTALLA03 = general.nz(Value, "")
            dvForm(PA).Row("TALLA03") = general.nz(Value, "") : guardarDV()
        End Set
    End Property

    Private mTALLA04 As String
    Public Property TALLA04() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA04 = general.nz(dvForm(PA).Row("TALLA04"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLA04, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mTALLA04 = general.nz(Value, "")
            dvForm(PA).Row("TALLA04") = general.nz(Value, "") : guardarDV()
        End Set
    End Property

    Private mTALLA05 As String
    Public Property TALLA05() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA05 = general.nz(dvForm(PA).Row("TALLA05"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLA05, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mTALLA05 = general.nz(Value, "")
            dvForm(PA).Row("TALLA05") = general.nz(Value, "") : guardarDV()
        End Set
    End Property

    Private mTALLA06 As String
    Public Property TALLA06() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA06 = general.nz(dvForm(PA).Row("TALLA06"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLA06, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mTALLA06 = general.nz(Value, "")
            dvForm(PA).Row("TALLA06") = general.nz(Value, "") : guardarDV()
        End Set
    End Property

    Private mTALLA07 As String
    Public Property TALLA07() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA07 = general.nz(dvForm(PA).Row("TALLA07"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLA07, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mTALLA07 = general.nz(Value, "")
            dvForm(PA).Row("TALLA07") = general.nz(Value, "") : guardarDV()
        End Set
    End Property

    Private mTALLA08 As String
    Public Property TALLA08() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA08 = general.nz(dvForm(PA).Row("TALLA08"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLA08, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mTALLA08 = general.nz(Value, "")
            dvForm(PA).Row("TALLA08") = general.nz(Value, "") : guardarDV()
        End Set
    End Property

    Private mTALLA09 As String
    Public Property TALLA09() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA09 = general.nz(dvForm(PA).Row("TALLA09"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLA09, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mTALLA09 = general.nz(Value, "")
            dvForm(PA).Row("TALLA09") = general.nz(Value, "") : guardarDV()
        End Set
    End Property

    Private mTALLA10 As String
    Public Property TALLA10() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA10 = general.nz(dvForm(PA).Row("TALLA10"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLA10, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mTALLA10 = general.nz(Value, "")
            dvForm(PA).Row("TALLA10") = general.nz(Value, "") : guardarDV()
        End Set
    End Property

    Private mREPRES As Integer
    Public Property REPRES() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mREPRES = nzn(dvForm(PA).Row("REPRES"), 0)
            Catch ex As Exception : End Try
            Return nzn(mREPRES, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If esCodigoExistente(dtRepres, CCRepres, Value) Then
                If nzn(Value, 0) <> 0 Then
                    mREPRES = nzn(Value, 0)
                    dvForm(PA).Row("NOMREPRES") = general.OBN(Value, dtRepres, CNRepres)
                    dvForm(PA).Row("REPRES") = nzn(Value, 0) : guardarDV()
                Else
                    dvForm(PA).Row("REPRES") = DBNull.Value

                    dvForm(PA).Row("NOMREPRES") = "" : guardarDV()
                End If
            Else
                dvForm(PA).Row("REPRES") = DBNull.Value

                dvForm(PA).Row("NOMREPRES") = "" : guardarDV()
                MessageBox.Show(rm.GetString("NOEXISTEREPRES"))
            End If
        End Set
    End Property

    Private mNOMREPRES As String
    Public Property NOMREPRES() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNOMREPRES = general.nz(dvForm(PA).Row("NOMREPRES"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMREPRES, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mNOMREPRES = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("NOMREPRES") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("NOMREPRES") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mCOMIS As Double
    Public Property COMIS() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mCOMIS = nzn(dvForm(PA).Row("COMIS"), 0)
            Catch ex As Exception : End Try
            Return nzn(mCOMIS, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            mCOMIS = nzn(Value, 0)
            dvForm(PA).Row("COMIS") = nzn(Value, 0) : guardarDV()
        End Set
    End Property
#End Region

#Region "VARIABLES"

    Public dtColores As New DataTable
    Public clienteElegido As Integer = -1

    Public detallePAF As clsDetallePAFCompraModelo
    Public detalleProcesos As clsProcesosOrden
    Public anulaciones As clsAnulaciones

    Private dvSeleccionModelos As New DataView

    Friend dtRepres As New DataTable("REPRES")
    Friend dtOperaciones As New DataTable("OPERACIONS")
    Friend dtTallers As New DataTable("TALLERS")
    Friend dtTemporadas As New DataTable("TEMPORADES")
    Friend dtModels As New DataTable("MODELS")
    Friend dtClients As New DataTable("CLIENTS")
    Friend dtClientConPAF As New DataTable

    Friend dtConsumos As New DataTable

#End Region

#Region "CAMPOS PROPIOS"

    Private mDIA1 As Integer
    Public Property DIA1() As Integer
        Get
            Try
                mDIA1 = general.nz(dvForm(PA).Row("DIA1"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDIA1, "")
        End Get
        Set(ByVal Value As Integer)
            mDIA1 = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("DIA1") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("DIA1") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mDIA2 As Integer
    Public Property DIA2() As Integer
        Get
            Try
                mDIA2 = general.nz(dvForm(PA).Row("DIA2"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDIA2, "")
        End Get
        Set(ByVal Value As Integer)
            mDIA2 = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("DIA2") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("DIA2") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mDIA3 As Integer
    Public Property DIA3() As Integer
        Get
            Try
                mDIA3 = general.nz(dvForm(PA).Row("DIA3"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDIA3, "")
        End Get
        Set(ByVal Value As Integer)
            mDIA3 = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("DIA3") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("DIA3") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mFORMA As String
    Public Property FORMA() As String
        Get
            Try
                mFORMA = general.nz(dvForm(PA).Row("FORMA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mFORMA, "")
        End Get
        Set(ByVal Value As String)
            mFORMA = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("FORMA") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("FORMA") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mcta As String
    Public Property cta() As String
        Get
            Try
                mcta = general.nz(dvForm(PA).Row("cta"), "")
            Catch ex As Exception : End Try
            Return general.nz(mcta, "")
        End Get
        Set(ByVal Value As String)
            mcta = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("cta") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("cta") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mDTOFORMA As Double
    Public Property DTOFORMA() As Double
        Get
            Try
                mDTOFORMA = nzn(dvForm(PA).Row("DTOFORMA"), 0)
            Catch ex As Exception : End Try
            Return nzn(mDTOFORMA, 0)
        End Get
        Set(ByVal Value As Double)
            mDTOFORMA = nzn(Value, 0)
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("DTOFORMA") = nzn(Value, 0) : guardarDV() : Debug.WriteLine(Value)
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("DTOFORMA") = nzn(Value, 0) : guardarDV() : Debug.WriteLine(Value)
            End If
        End Set
    End Property

    Private mREPRECLIENT As Integer
    Public Property REPRECLIENT() As Integer
        Get
            Try
                mREPRECLIENT = nzn(dvForm(PA).Row("REPRECLIENT"), 0)
            Catch ex As Exception : End Try
            Return nzn(mREPRECLIENT, 0)
        End Get
        Set(ByVal Value As Integer)
            mREPRECLIENT = nzn(Value, 0)
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("REPRECLIENT") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("REPRECLIENT") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mBANC As String
    Public Property BANC() As String
        Get
            Try
                mBANC = general.nz(dvForm(PA).Row("BANC"), "")
            Catch ex As Exception : End Try
            Return general.nz(mBANC, "")
        End Get
        Set(ByVal Value As String)
            mBANC = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("BANC") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("BANC") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mCOFI As String
    Public Property COFI() As String
        Get
            Try
                mCOFI = general.nz(dvForm(PA).Row("COFI"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCOFI, "")
        End Get
        Set(ByVal Value As String)
            mCOFI = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("COFI") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("COFI") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mOFI As String
    Public Property OFI() As String
        Get
            Try
                mOFI = general.nz(dvForm(PA).Row("OFI"), "")
            Catch ex As Exception : End Try
            Return general.nz(mOFI, "")
        End Get
        Set(ByVal Value As String)
            mOFI = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("OFI") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("OFI") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mDC As String
    Public Property DC() As String
        Get
            Try
                mDC = general.nz(dvForm(PA).Row("DC"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDC, "")
        End Get
        Set(ByVal Value As String)
            mDC = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("DC") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("DC") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mNOMBANC As String
    Public Property NOMBANC() As String
        Get
            Try
                mNOMBANC = general.nz(dvForm(PA).Row("NOMBANC"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMBANC, "")
        End Get
        Set(ByVal Value As String)
            mNOMBANC = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("NOMBANC") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("NOMBANC") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mNUMEROPAG As Integer
    Public Property NUMEROPAG() As Integer
        Get
            Try
                mNUMEROPAG = nzn(dvForm(PA).Row("NUMEROPAG"), 0)
            Catch ex As Exception : End Try
            Return nzn(mNUMEROPAG, 0)
        End Get
        Set(ByVal Value As Integer)
            mNUMEROPAG = nzn(Value, 0)
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("NUMEROPAG") = nzn(Value, 0) : guardarDV() : Debug.WriteLine(Value)
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("NUMEROPAG") = nzn(Value, 0) : guardarDV() : Debug.WriteLine(Value)
            End If
        End Set
    End Property
    Private mDIESPAG As Integer
    Public Property DIESPAG() As Integer
        Get
            Try
                mDIESPAG = nzn(dvForm(PA).Row("DIESPAG"), 0)
            Catch ex As Exception : End Try
            Return nzn(mDIESPAG, 0)
        End Get
        Set(ByVal Value As Integer)
            mDIESPAG = nzn(Value, 0)
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("DIESPAG") = nzn(Value, 0) : guardarDV() : Debug.WriteLine(Value)
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("DIESPAG") = nzn(Value, 0) : guardarDV() : Debug.WriteLine(Value)
            End If
        End Set
    End Property

    Private mVEZ1FORPAG As Integer
    Public Property VEZ1FORPAG() As Integer
        Get
            Try
                mVEZ1FORPAG = nzn(dvForm(PA).Row("VEZ1FORPAG"), 0)
            Catch ex As Exception : End Try
            Return nzn(mVEZ1FORPAG, 0)
        End Get
        Set(ByVal Value As Integer)
            mVEZ1FORPAG = nzn(Value, 0)
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("VEZ1FORPAG") = nzn(Value, 0) : guardarDV() : Debug.WriteLine(Value)
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("VEZ1FORPAG") = nzn(Value, 0) : guardarDV() : Debug.WriteLine(Value)
            End If
        End Set
    End Property

    Private mNOMFORMA As String
    Public Property NOMFORMA() As String
        Get
            Try
                mNOMFORMA = general.nz(dvForm(PA).Row("NOMFORMA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMFORMA, "")
        End Get
        Set(ByVal Value As String)
            mNOMFORMA = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("NOMFORMA") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("NOMFORMA") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mCOMISREPRESENTANT As Double
    Public Property COMISREPRESENTANT() As Double
        Get
            Try
                mCOMISREPRESENTANT = nzn(dvForm(PA).Row("COMISREPRESENTANT"), 0)
            Catch ex As Exception : End Try
            Return nzn(mCOMISREPRESENTANT, 0)
        End Get
        Set(ByVal Value As Double)
            mCOMISREPRESENTANT = nzn(Value, 0)
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("COMISREPRESENTANT") = nzn(Value, 0) : guardarDV() : Debug.WriteLine(Value)
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("COMISREPRESENTANT") = nzn(Value, 0) : guardarDV() : Debug.WriteLine(Value)
            End If
        End Set
    End Property


#End Region

    Public Sub New(ByVal tabla As DataTable, _
               ByVal centro As String, ByRef bindingcontext As BindingContext, ByVal tipo As String)

        MyBase.New(tabla, centro, bindingcontext, "ORDRE")
        Dim sqlSel As String
        Try
            ACN()
            'GENERICO
            sqlSinWhere = "SELECT CORDRE.* " & _
                            " FROM CORDRE "
            sqlSel = sqlSinWhere & genWhere() & " LIMIT 0,1"


            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel
            da.Fill(tabla)
            numeroRegistros = obtenerNumeroReg("cordre", "")
            numeroRegistroActual = 0
            dvForm = tabla.DefaultView
            dvForm.Sort = "ORDRE"


            If Not tabla.Columns.Contains("REPRECLIENT") Then
                tabla.Columns.Add(New DataColumn("DIA1"))
                tabla.Columns.Add(New DataColumn("DIA2"))
                tabla.Columns.Add(New DataColumn("DIA3"))
                tabla.Columns.Add(New DataColumn("FORMA"))
                tabla.Columns.Add(New DataColumn("cta"))
                tabla.Columns.Add(New DataColumn("REPRECLIENT"))
                tabla.Columns.Add(New DataColumn("DTOFORMA"))
                tabla.Columns.Add(New DataColumn("BANC"))
                tabla.Columns.Add(New DataColumn("NOMBANC"))
                tabla.Columns.Add(New DataColumn("DC"))
                tabla.Columns.Add(New DataColumn("OFI"))
                tabla.Columns.Add(New DataColumn("COFI"))
                tabla.Columns.Add(New DataColumn("NUMEROPAG"))
                tabla.Columns.Add(New DataColumn("DIESPAG"))
                tabla.Columns.Add(New DataColumn("VEZ1FORPAG"))
                tabla.Columns.Add(New DataColumn("NOMFORMA"))
                tabla.Columns.Add(New DataColumn("COMISREPRESENTANT"))
            End If

            CrearTabladtColores()

            IniciarDetalles()
            CargarTablas()
            MoverActual()
            tabla.AcceptChanges()
            CCN()


        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Sub New(ByVal tabla As DataTable, _
               ByVal centro As String, ByRef bindingcontext As BindingContext, ByVal tipo As String, ByVal soloclase As Boolean, ByVal numero As Double)

        MyBase.New(tabla, centro, bindingcontext, "ORDRE")
        Dim sqlSel As String
        Try
            ACN()
            'GENERICO
            Me.soloClase = soloclase
            sqlSinWhere = "SELECT CORDRE.* " & _
                            " FROM CORDRE "
            sqlSel = sqlSinWhere & genWhere() & " AND ORDRE = " & numero & " LIMIT 0,1"

            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel
            da.Fill(tabla)
            numeroRegistros = obtenerNumeroReg("cordre", "")
            numeroRegistroActual = 0
            dvForm = tabla.DefaultView
            dvForm.Sort = "ORDRE"


            If Not tabla.Columns.Contains("REPRECLIENT") And Not soloclase Then
                tabla.Columns.Add(New DataColumn("DIA1"))
                tabla.Columns.Add(New DataColumn("DIA2"))
                tabla.Columns.Add(New DataColumn("DIA3"))
                tabla.Columns.Add(New DataColumn("FORMA"))
                tabla.Columns.Add(New DataColumn("cta"))
                tabla.Columns.Add(New DataColumn("REPRECLIENT"))
                tabla.Columns.Add(New DataColumn("DTOFORMA"))
                tabla.Columns.Add(New DataColumn("BANC"))
                tabla.Columns.Add(New DataColumn("NOMBANC"))
                tabla.Columns.Add(New DataColumn("DC"))
                tabla.Columns.Add(New DataColumn("OFI"))
                tabla.Columns.Add(New DataColumn("COFI"))
                tabla.Columns.Add(New DataColumn("NUMEROPAG"))
                tabla.Columns.Add(New DataColumn("DIESPAG"))
                tabla.Columns.Add(New DataColumn("VEZ1FORPAG"))
                tabla.Columns.Add(New DataColumn("NOMFORMA"))
                tabla.Columns.Add(New DataColumn("COMISREPRESENTANT"))
            End If

            CrearTabladtColores()

            IniciarDetalles()
            'CargarTablas()
            MoverActual()
            tabla.AcceptChanges()
            CCN()


        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub CargarClientesConPAF()
        Dim cmd As New MySqlCommand("SELECT DISTINCT CLIENT AS CODI FROM CORDRE WHERE CENTRO = """ & centro & """ ", cnn)
        Dim dar As MySqlDataReader
        Dim dr As DataRow
        Dim dc1 As New DataColumn("CODI")
        Dim dc2 As New DataColumn("NOM")
        Dim i As Integer
        Try
            ACN()
            dar = cmd.ExecuteReader
            dtClientConPAF = New DataTable
            dtClientConPAF.Columns.Clear()
            dtClientConPAF.Columns.Add(dc1)
            dtClientConPAF.Columns("CODI").Caption = rm.GetString("CODIGO")
            dtClientConPAF.Columns.Add(dc2)
            dtClientConPAF.Columns("NOM").Caption = rm.GetString("NOMBRE")

            While dar.Read
                dr = dtClientConPAF.NewRow
                dr("CODI") = dar("CODI")
                dtClientConPAF.Rows.Add(dr)
            End While

            'Le ponemos los nombres a los proveedores
            For i = 0 To dtClientConPAF.Rows.Count - 1
                dtClientConPAF.Rows(i).Item("NOM") = general.OBN(dtClientConPAF.Rows(i).Item("CODI"), dtClients)
            Next
            dtClientConPAF.DefaultView.Sort = "NOM"
            dar.Close()
            CCN()

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Public Overrides Sub CargarIdentificadores()
        Dim a As New ArrayList
        Dim i As Integer
        Try
            a.Add("ORDRE")
            a.Add("CLIENT")

            If Not clienteElegido = -1 Then
                CargaTabla(tablaOrdenesModelos, a, dtIdentificadores, True, " CLIENT = """ & CLIENT & """ ") 'AND CENTRO = """ & centro & """ ")
            Else
                CargaTabla(tablaOrdenesModelos, a, dtIdentificadores, True) '" CENTRO = """ & centro & """ ")
            End If

            'Le ponemos los nombres a los proveedores
            dtIdentificadores.Columns("ORDRE").Caption = rm.GetString("NUMERO")
            dtIdentificadores.Columns("CLIENT").Caption = rm.GetString("CLIENTE")
            If Not dtIdentificadores.Columns.Contains("NOMCLIENT") Then dtIdentificadores.Columns.Add(New DataColumn("NOMCLIENT"))
            dtIdentificadores.Columns("NOMCLIENT").Caption = rm.GetString("NOMCLIENT")
            For i = 0 To dtIdentificadores.Rows.Count - 1
                dtIdentificadores.Rows(i).Item("NOMCLIENT") = general.OBN(dtIdentificadores.Rows(i).Item("CLIENT"), dtClients)
            Next
            dvIdentificadores = dtIdentificadores.DefaultView
            dvIdentificadores.RowFilter = "CENTRO = '" & centro & "' "
            dvIdentificadores.Sort = "ORDRE"

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

#Region "SELECCION PAF"

    Public Sub ElegirCliente(ByVal cliente As Integer)
        Try
            ACN()
            clienteElegido = cliente
            Me.ActualizarOrigen(True, False)
            numeroRegistros = obtenerNumeroReg(t, "")
            If cliente <> -1 Then
                If numeroRegistros = 0 Then Exit Sub
                Me.CargarRegistro(primero, esCambioSeleccion)
            Else
                Me.CargarRegistro(ultimo)
            End If
            CargarIdentificadores()
            MoverActual()
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "ORGANIZAR"

    Private Sub IniciarDetalles()

        Dim dt As DataTable
        Try
            dt = New aura2k3.ds11.ordreDataTable
            detallePAF = New clsDetallePAFCompraModelo(dt, centro, bc, Me)

            dt = New aura2k3.ds11.feinesDataTable
            detalleProcesos = New clsProcesosOrden(dt, centro, bc, Me)

            dt = New aura2k3.ds11.anulDataTable
            anulaciones = New clsAnulaciones(dt, centro, bc, Me)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub CargarTablas()
        Try
            CargaTabla("treballs", "CODI", "DESCRI", dtOperaciones, True)
            CargaTabla(tablaTalleres, CCTallers, CNTallers, dtTallers, True)
            CargaTablaRepresentantes()
            CargaTablaClientes()
            CargaTabla(tablaModelos, "CODI", "DESCRI", dtModels, True)
            CargaTabla(tablaTemporadas, "CODI", "DESCRI", dtTemporadas, True)
            CargarClientesConPAF()
            CargarIdentificadores()
            'CargaTabla(tablaClientes, CCClients, CNClients, dtClients, True)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub CargaTablaRepresentantes()
        Try
            Dim cmdSelect As New MySqlCommand
            Dim daSelect As New MySqlDataAdapter

            cmdSelect.Connection = cnn
            cmdSelect.CommandText = "SELECT CODI, NOM, COMIS FROM REPRES"

            daSelect.SelectCommand = cmdSelect
            ACN()
            dtRepres.Rows.Clear()
            daSelect.Fill(dtRepres)
            dtRepres.DefaultView.Sort = "CODI"

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub MoverActual()
        Try
            detallePAF.CambioDetalle(centro, Me)
            anulaciones.CambioDetalle(centro, Me)
            detalleProcesos.CambioDetalle(centro, Me)

            CargarColoresregistroActual()
            PonerFiltroIdentificadores()
            If PA() <> -1 Then PonerNombres()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

    Private Sub CargaTablaClientes()
        Try
            Dim cmdSelect As New MySqlCommand
            Dim daSelect As New MySqlDataAdapter

            cmdSelect.Connection = cnn
            cmdSelect.CommandText = _
                        " SELECT clients.CODI, clients.NOM, " & _
                        " IVA.IVA, IVA.RE, " & _
                        " clients.DIA1,clients.DIA2,clients.DIA3, " & _
                        " clients.FORMA,clients.CTA,clients.DTOFORMA,  " & _
                        " clients.REPRES AS REPRECLIENT, clients.REPRES, clients.BANC, clients.COFI, clients.OFI, clients.CENTRO, " & _
                        " clients.DC FROM CLIENTS LEFT JOIN IVA ON (IVA.CODI = CLIENTS.IVA) "

            daSelect.SelectCommand = cmdSelect
            ACN()
            dtClients.Rows.Clear()
            daSelect.Fill(dtClients)
            dtClients.DefaultView.Sort = "CODI"

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub PonerNombres()
        Dim aceptarCambiosAlFinal As Boolean = False
        Try
            If Not soloClase Then

                If tabla.GetChanges Is Nothing Then aceptarCambiosAlFinal = True
                NOMCLIENT = general.OBN(CLIENT, dtClients)
                'BANC = general.OBN(CLIENT, dtClients, "BANC")
                REPRECLIENT = general.OBN(CLIENT, dtClients, "REPRECLIENT", "CODI", True)
                REPRES = general.OBN(REPRECLIENT, dtClients, "REPRECLIENT", "CODI", True)
                If REPRES <> REPRECLIENT Then
                    'REPRES = REPRECLIENT
                End If
                'NOMREPRES = general.OBN(REPRES, dtRepres)

                'DIA1 = general.OBN(CLIENT, dtClients, "DIA1", "CODI", True)
                'DIA2 = general.OBN(CLIENT, dtClients, "DIA2", "CODI", True)
                'DIA3 = general.OBN(CLIENT, dtClients, "DIA3", "CODI", True)
                'FORMA = general.OBN(CLIENT, dtClients, "FORMA")
                'cta = general.OBN(CLIENT, dtClients, "CTA")
                'COFI = general.OBN(CLIENT, dtClients, "COFI")
                'OFI = general.OBN(CLIENT, dtClients, "OFI")
                'DTOFORMA = general.OBN(CLIENT, dtClients, "DTOFORMA", "CODI", True)


                'NOMBANC = general.OBN(BANC, dtBancs, "DESCRIPCIO")
                If COMIS <> general.OBN(REPRES, dtRepres, "COMIS", "CODI", True) Then
                    COMIS = general.OBN(REPRES, dtRepres, "COMIS", "CODI", True)
                End If


                COMISREPRESENTANT = general.OBN(CLIENT, dtRepres, "COMIS", "CODI", True)
                'NUMEROPAG = general.OBN(FORMA, dtForpag, "NUMEROPAG", "CODI", True)
                'DIESPAG = general.OBN(FORMA, dtForpag, "DIESPAG", "CODI", True)
                'VEZ1FORPAG = general.OBN(FORMA, dtForpag, "VEZ1FORPAG", "CODI", True)
                'NOMFORMA = general.OBN(FORMA, dtForpag, "NOMFORMA")


                'P_IVA1 = general.OBN(CLIENT, dtClients, "IVA", "CODI", True)
                'P_RE1 = general.OBN(CLIENT, dtClients, "RE")

                If aceptarCambiosAlFinal AndAlso (Not tabla.GetChanges Is Nothing) Then tabla.AcceptChanges()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
        End Try
    End Sub
    Private Function ComprobarActualizacion() As Double
        Dim numer As Double
        Try
            numer = GetNumeroUltimoPAF("P", centro) + 1
            If numer <> ORDRE Then
                Return numer
            End If
            Return ORDRE

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Function
    Public Sub actualizarNumeraciones()
        Try
            'ORDRE = ComprobarActualizacion()
            ActualizarNum("ORDRE", OrdenModelos, centro, ORDRE)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Public Overrides Sub ActualizarOrigen(Optional ByVal nocerrar As Boolean = False, Optional ByVal hackDetalle As Boolean = False)
        Try
            MyBase.ActualizarOrigen(True)
            detallePAF.ActualizarOrigen(True, True)
            detalleProcesos.ActualizarOrigen(True, True)
            CCN()

        Catch ex As Exception
            If ex.ToString.Substring(0, 9) = "Duplicate" Then
                MessageBox.Show("ORDRE JA EXISTENT")
                'La única posibilidad de que este dupliacado es que alguien haya insertado otro antes
            End If
            Throw ex
            'LOG(ex.ToString) 
            cargando = False : CCN()
        End Try
    End Sub
    Private Function obtenerRowFilter() As String
        Return "MODEL = '" & MODEL & "' AND SERIE = '" & SERIE & "' AND TEMPORADA = '" & TEMPORADA & "' AND CLIENT = '" & CLIENT & "' "
    End Function

    Private Function CrearTablaSeleccion() As DataTable
        Dim dt As New DataTable
        Try
            dt.Columns.Add(New DataColumn("MODEL", GetType(String)))
            dt.Columns.Add(New DataColumn("NOM", GetType(String)))
            dt.Columns.Add(New DataColumn("CLIENT", GetType(Integer)))
            dt.Columns.Add(New DataColumn("SERIE", GetType(String)))
            dt.Columns.Add(New DataColumn("TEMPORADA", GetType(String)))

            Return dt
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Public Sub CargarSeriesTemporadas()
        Dim dar As MySqlDataReader
        Dim cmdSel As New MySqlCommand("SELECT DISTINCT MODELS.CLIENT, " & _
                " MODELS.SERIE, MODELS.TEMPORADA " & _
                " FROM MODELS " & _
                " WHERE MODELS.CODI = """ & MODEL & """", cnn)
        Dim dt As DataTable
        Dim i As Integer
        Dim dr As DataRow
        Try

            ACN()
            dt = CrearTablaSeleccion()
            dvSeleccionModelos = dt.DefaultView
            dvSeleccionModelos.Sort = "MODEL, TEMPORADA, SERIE, CLIENT"

            dar = cmdSel.ExecuteReader

            While dar.Read

                dr = dt.NewRow
                dr("SERIE") = dar("SERIE")
                dr("CLIENT") = dar("CLIENT")
                dr("TEMPORADA") = dar("TEMPORADA")
                dr("MODEL") = MODEL
                dt.Rows.Add(dr)

            End While

            CCN()
            dar.Close()
            For i = 0 To dvSeleccionModelos.Count - 1
                dvSeleccionModelos(i).Item("NOM") = general.OBN(dvSeleccionModelos(i).Item("CLIENT"), dtClients)
            Next

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN() : dar.Close()
        End Try
    End Sub
    Public Sub TratarSeleccion()
        Try
            If TEMPORADA <> "" And SERIE <> "" And CLIENT <> 0 And CLIENT <> -1 And MODEL <> "" Then
                CargarColoresregistroActual()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerFiltroIdentificadores()
        Dim filter As String = ""
        Try
            'If clienteElegido <> -1 Then filter = "CLIENT = '" & clienteElegido & "' "
            'dvIdentificadores.RowFilter = filter
            If clienteElegido = -1 Then : dvIdentificadores.RowFilter = ""
            Else : dvIdentificadores.RowFilter = "CLIENT = '" & clienteElegido & "'" : End If

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

    Public Sub TraspasarLinea(ByVal p As Point)
        Dim f As New frmTraspasoLineasOrdenes
        Dim dv As New DataView
        Try
            If ALB = False Then
                'Ahora hay que tener cuidado al poner el TIPO no es el del PAF es el de la linea
                f.PAF = Me
                'If Not IsDBNull(lineasVenta.NTRAS) Then
                '    If lineasVenta.NTRAS < 0 Then
                '        'f.cantidadATraspasar = lineasVenta.NTRAS
                '    Else
                '        'f.cantidadATraspasar = lineasVenta.UNITATS - lineasVenta.NTRAS
                '    End If
                'Else
                'f.cantidadATraspasar = lineasVenta.UNITATS
                f.localizacion = p
                f.ShowDialog()

                'ActualizarOrigen(tablaLineasVenta, daLineasPedido)
                f = Nothing
            Else
                MessageBox.Show(rm.GetString("NOTRASPLINIASDOCTRAS"))
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub CrearTabladtColores()
        Dim dcMODCOL As New DataColumn("MODCOL")
        Dim dcSERIE As New DataColumn("SERIE")
        Dim dcCLIENT As New DataColumn("CLIENT")
        Dim dcMODEL As New DataColumn("MODEL")
        Dim dcTEMPORADA As New DataColumn("TEMPORADA")
        Dim dcCENTRO As New DataColumn("CENTRO")
        dtColores.Columns.Add(dcMODCOL)
        dtColores.Columns("MODCOL").Caption = rm.GetString("COLOR")
        dtColores.Columns.Add(dcSERIE)
        dtColores.Columns.Add(dcCLIENT)
        dtColores.Columns.Add(dcMODEL)
        dtColores.Columns.Add(dcTEMPORADA)
        dtColores.Columns.Add(dcCENTRO)
    End Sub
    Private Sub CargarColoresregistroActual() '(ByRef cbo As C1.Win.C1List.C1Combo, Optional ByVal deiniciardg As Boolean = False)
        Dim dar As MySqlDataReader
        Dim dr As DataRow
        Dim cmdSel As New MySqlCommand(" SELECT DISTINCT MODCOL FROM MODCOL " & _
                    " WHERE MODEL = """ & MODEL & """ AND " & _
                    " CLIENT = """ & CLIENT & """ AND " & _
                    " TEMPORADA = """ & TEMPORADA & """ AND " & _
                    " SERIE = """ & SERIE & """ " & _
                    " AND CENTRO = """ & centro & """ ", cnn)
        Try

            ACN()
            dar = cmdSel.ExecuteReader
            dtColores.Clear()
            While dar.Read
                dr = dtColores.NewRow
                dr("MODCOL") = dar("MODCOL")
                dtColores.Rows.Add(dr)
            End While
            dar.Close()
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN() : dar.Close()
        End Try
    End Sub
    Public Overloads Function cambioCentro(ByVal centro As Char, ByVal iralregistro As Integer) As Boolean
        Try
            MyBase.cambioCentro(centro, iralregistro)
            CargarClientesConPAF()
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Function

#End Region

#Region "DESPLAZAMIENTO"

    Public Sub DormirHandlers()

    End Sub
    Public Sub DespertarHandlers()

    End Sub
    Public Overrides Sub SiguienteReg()
        Try
            DormirHandlers()
            ActualizarOrigen()
            CargarRegistro(siguiente)
            MoverActual()
            DespertarHandlers()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub AnteriorReg()
        Try
            DormirHandlers()
            ActualizarOrigen()
            CargarRegistro(anterior)
            MoverActual()
            DespertarHandlers()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub UltimoReg()
        Try
            DormirHandlers()
            ActualizarOrigen()
            CargarRegistro(ultimo)
            MoverActual()
            DespertarHandlers()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub PrimeroReg()
        Try
            DormirHandlers()
            ActualizarOrigen()
            CargarRegistro(primero)
            MoverActual()
            DespertarHandlers()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub CambiarAReg(ByVal id As String, ByVal accion As Integer)
        Dim sql As String
        Try
            DormirHandlers()
            ActualizarOrigen()
            MyBase.CambiarAReg(id, "", accion)
            MoverActual()
            DespertarHandlers()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "OVERRIDES"

    Friend Overrides Function TieneCambios() As Boolean
        Try
            guardarDV()
            If Not tabla.GetChanges Is Nothing OrElse detallePAF.TieneCambios OrElse detalleProcesos.TieneCambios Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Private Function HaySeleccion() As Boolean
        Try
            If clienteElegido = -1 Then Return False
            Return True
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function genWhere() As String
        Dim ret As String
        Try

            If HaySeleccion() Then
                ret = " WHERE CORDRE.CENTRO = """ & general.nz(centro, empresaPorDefecto) & """ AND CLIENT = " & clienteElegido & " "
            Else
                ret = " WHERE CORDRE.CENTRO = """ & general.nz(centro, empresaPorDefecto) & """ "
            End If

            Return ret

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function GenOrder() As String
        Try
            Return " ORDER BY ORDRE "
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Function genWhereSubSelect(ByVal esCambio As Boolean) As String
        Try
            If esCambio AndAlso Not HaySeleccion() Then
                Return " M1.CLIENT = " & clienteElegido & " AND CENTRO = """ & centro & """ AND "
            Else
                If HaySeleccion() Then
                    Return " CLIENT = " & clienteElegido & " AND CENTRO = """ & centro & """ AND "
                Else
                    Return " CENTRO = """ & centro & """ AND "
                End If
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Private Function GenWhereConTodo(ByVal id As Object, ByVal esCambio As Boolean) As String
        Dim ret As String
        Try

            If Not HaySeleccion() Then 'AndAlso Not esCambio Then
                'Debe ser un cambio Como id seguro que no será nothing mirmaos cual es el numero
                'Aqui nunca se llega
                ret = " WHERE ORDRE = """ & id & """ AND " & WCNoTabla() & " "
            Else
                'Que sea nothing quiere decir que ha habido un ir a registro
                If id Is Nothing OrElse esCambio Then
                    ret = " WHERE  " & WCNoTabla() & " AND CLIENT = """ & clienteElegido & """ "
                Else
                    ret = " WHERE  " & WCNoTabla() & " " & _
                            " AND ORDRE = """ & id & """ AND CLIENT = """ & clienteElegido & """ "
                End If
            End If

            Return ret
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Function genWhereSeleccion(ByVal tabla As String) As String
        Try
            Dim ret As String
            Dim i As Integer
            ret = " WHERE CORDRE.CENTRO = """ & general.nz(centro, empresaPorDefecto) & """  "

            If clienteElegido <> -1 Then
                ret = ret & " AND " & tabla & "CLIENT = " & clienteElegido & " "
            End If

            Return ret

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function ObtenerNumeroRegistro(ByVal id As Object) As Integer
        Dim esCambio As Boolean = False
        Dim idx As Object
        If id Is Nothing Then : id = ORDRE : Else
            If id.GetType Is GetType(Integer) Then
                If id = esCambioSeleccion Then : esCambio = True : End If
            End If
        End If

        Dim cmd As New MySqlCommand(" SELECT (SELECT COUNT(*) " & _
                            " FROM " & t() & " AS M2 WHERE " & genWhereSubSelect(esCambio) & " " & _
                            " M2.ORDRE < M1.ORDRE) AS rownum FROM " & t() & " AS M1 " & _
                            " " & GenWhereConTodo(id, esCambio) & " " & GenOrder(), cnn)
        Try
            idx = cmd.ExecuteScalar()
            If idx Is Nothing Then Return -1

            Return idx '- 1

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function genWhereNumeroRegistros() As String
        Dim ret As String
        Try
            If Not HaySeleccion() Then
                ret = " WHERE CORDRE.CENTRO = """ & general.nz(centro, empresaPorDefecto) & """ "
            Else
                ret = genWhereSeleccion("")
            End If

            Return ret
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Public Overrides Sub NuevoRegistro()
        Dim drNew As DataRow
        Try
            ActualizarOrigen()
            tabla.Clear()
            drNew = tabla.NewRow()
            drNew.Item("ORDRE") = GetNumeroUltimoPAF("P", centro) + 1
            drNew.Item("CLIENT") = 0
            drNew.Item("SERIE") = ""
            drNew.Item("CENTRO") = centro
            drNew.Item("ALB") = False
            drNew.Item("TEMPORADA") = ""

            tabla.Rows.Add(drNew)
            numeroRegistros = numeroRegistros + 1
            Try
                guardarDV()
            Catch ex As Exception
            End Try
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overrides Sub borrar()
        Try

            MyBase.borrar()
            detallePAF.borrar() : ActualizarOrigen()
            detalleProcesos.borrar() : ActualizarOrigen()
            anulaciones.borrar() : ActualizarOrigen()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "IMPRESION"

    Public Sub ImprimirPAF()
        Dim clsImp As clsImpresionPAFPO
        Try
            clsImp = New clsImpresionPAFPO(OrdenModelos, Muestra, Venta, Me.dvForm, detallePAF.dvForm, False, Nothing, True)
            clsImp.ImprimirDocumento(False)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "TRASPASOS"

    Private Sub CrearNuevoAlbaran(ByVal NUMERO As Integer, ByVal DATA As Date)
        Dim cmdIns As New MySqlCommand
        Dim P_IVA1 As Double = nzn(DLookUp("IVA", "IVA", " CODI =""" & general.nz(DLookUp("IVA", "CLIENTS", " CODI =""" & CLIENT & """ "), "") & """ "), 0)
        Dim base1, brut1 As Double
        Dim IVA1 As Double
        Dim SQL As String
        Try

            'El import es la suma de las cantidades por el precio
            Dim sumaUnidades As Double = sumaTotal("QUANTITAT", detallePAF.dvForm)
            base1 = (sumaUnidades * PREU)
            brut1 = base1
            Dim total As Double = roundnum(base1 * (1 + P_IVA1 / 100), 2)
            IVA1 = roundnum(base1 * (P_IVA1 / 100), 2)
            SQL = "INSERT INTO FACTUR (" & _
                " TIPUS, " & _
                " DOCUMENT, " & _
                " FRA, " & _
                " ORDRE, " & _
                " DATA, " & _
                " CLIENT, " & _
                " PREU, " & _
                " TALLA01,TALLA02,TALLA03,TALLA04,TALLA05,TALLA06,TALLA07,TALLA08,TALLA09,TALLA10, " & _
                " TRASPAS, " & _
                " MODEL, " & _
                " SERIE, " & _
                " EXPEDICIO, " & _
                " ALBCLI, " & _
                " OBSERV, " & _
                " REPRES, " & _
                " CENTRO, " & _
                " COMIS, " & _
                " BASE1, " & _
                " P_IVA1, " & _
                " TOTAL, " & _
                " BRUT1, IVA1 " & _
                " ) VALUES(  " & _
                " ""P"", " & _
                " ""A"", " & _
                " " & NUMERO & ", " & _
                " " & ORDRE & ", " & _
                " """ & ConvertirAfechaMysql(DATA) & """,  " & _
                " " & CLIENT & ", " & _
                " """ & general.NS(PREU, True) & """,  " & _
                " """ & TALLA01 & """,""" & TALLA02 & """,""" & TALLA03 & """, " & _
                " """ & TALLA04 & """,""" & TALLA05 & """,""" & TALLA06 & """, " & _
                " """ & TALLA07 & """,""" & TALLA08 & """,""" & TALLA09 & """, " & _
                " """ & TALLA10 & """," & _
                " 0, " & _
                " """ & MODEL & """, " & _
                " """ & SERIE & """, " & _
                " """ & EXPEDICIO & """, " & _
                " """ & ALBARA & """, " & _
                " """", " & _
                " " & general.NS(REPRES, True) & ", " & _
                " """ & centro & """, " & _
                " " & general.NS(COMIS, True) & ", " & _
                " " & general.NS(base1, True) & ", " & _
                " " & general.NS(P_IVA1, True) & ", " & _
                " " & general.NS(total, True) & ", " & _
                " " & general.NS(brut1, True) & ", " & _
                " " & general.NS(IVA1, True) & ")"

            cmdIns.CommandText = SQL
            cmdIns.Connection = cnn
            ACN()
            cmdIns.ExecuteNonQuery()
            CCN()
            ActualizarNum("FACTUR", Albaran, centro, NUMERO)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Function ComprobarAlbaranATraspasar(ByVal aAlbaran As Double, ByVal aFecha As Date) As Integer
        Dim cmdSel As New MySqlCommand("SELECT COUNT(FRA) FROM FACTUR " & _
                    " WHERE FRA = """ & nzn(aAlbaran, 0) & """ AND " & _
                    " DOCUMENT = ""A"" AND " & _
                    " TIPUS = ""P"" AND " & _
                    " CENTRO = """ & centro & """ ", cnn)
        Dim cmdSel2 As New MySqlCommand("SELECT TRASPAS FROM FACTUR " & _
                    " WHERE " & _
                    " FRA = """ & nzn(aAlbaran, 0) & """ AND " & _
                    " DOCUMENT = ""A"" AND " & _
                    " TIPUS = ""P"" AND " & _
                    " CENTRO = """ & centro & """ ", cnn)
        Try
            ACN()
            If nzn(cmdSel.ExecuteScalar, 0) = 0 AndAlso nzn(aAlbaran, 0) <> 0 Then
                'Quiere decir que tendremos que crear un nuevo albarán
                CrearNuevoAlbaran(aAlbaran, aFecha)
                Return aAlbaran
            Else
                'Hay que trasasar a un albaran existente, por lo que hay hacer la
                'comprobación de ates
                If cmdSel2.ExecuteScalar = True Then
                    'Si ya esta traspasado no se puede hacer
                    MsgBox(rm.GetString("ALBARANYATRASPASADO"), MsgBoxStyle.OkOnly)
                    Return -1
                Else
                    Return aAlbaran
                End If
            End If
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Private Function ObtenerTara(ByVal talla As String) As Double
        'Esta funcion devuelve las taras
        Dim i As Double = 0
        Dim tara As Double = 0
        Try
            For i = 0 To anulaciones.dvForm.Count - 1
                If anulaciones.dvForm(i).Item("TALLA") = talla Then
                    tara = tara + anulaciones.dvForm(i).Item("UNIT")
                End If
            Next

            Return tara

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Private Sub TraspasarAAlbaran(ByVal NUMERO As String)
        'Hay que añadir una línea ALBA con el NUMERO pasado
        'Esto por cada linea del detalle restandole a la cantidad las taras y anulaciones
        Dim cmdIns As New MySqlCommand
        Dim cmdSel As MySqlCommand
        Dim nlineas As Integer
        Dim import As Double
        Dim SQL As String
        Dim i As Integer
        Dim unidadesTotales As Double
        Try
            ACN()
            cmdSel = New MySqlCommand("SELECT MAX(NLINEA) FROM DFACTU WHERE FRA = """ & NUMERO & """ AND CENTRO = """ & centro & """ AND DOCUMENT = ""A"" AND TIPUS = ""P"" ", cnn)
            nlineas = nzn(cmdSel.ExecuteScalar, 0)
            cmdIns.Connection = cnn

            For i = 0 To detallePAF.dvForm.Count - 1
                unidadesTotales = nzn(detallePAF.dvForm(i).Item("TALL1"), 0) + _
                    nzn(detallePAF.dvForm(i).Item("TALL2"), 0) + _
                    nzn(detallePAF.dvForm(i).Item("TALL3"), 0) + _
                    nzn(detallePAF.dvForm(i).Item("TALL4"), 0) + _
                    nzn(detallePAF.dvForm(i).Item("TALL5"), 0) + _
                    nzn(detallePAF.dvForm(i).Item("TALL6"), 0) + _
                    nzn(detallePAF.dvForm(i).Item("TALL7"), 0) + _
                    nzn(detallePAF.dvForm(i).Item("TALL8"), 0) + _
                    nzn(detallePAF.dvForm(i).Item("TALL9"), 0) + _
                    nzn(detallePAF.dvForm(i).Item("TALL10"), 0)

                SQL = "INSERT INTO DFACTU (" & _
                    " FRA," & _
                    " DOCUMENT," & _
                    " TIPUS," & _
                    " COLOR," & _
                    " NLINEA," & _
                    " TALLA01,TALLA02,TALLA03,TALLA04,TALLA05,TALLA06,TALLA07,TALLA08,TALLA09,TALLA10," & _
                    " TARA1,TARA2,TARA3,TARA4,TARA5,TARA6,TARA7,TARA8,TARA9,TARA10," & _
                    " ESTOC, " & _
                    " IMPORT, " & _
                    " CENTRO, UNITATS " & _
                    " ) VALUES(" & _
                    " " & NUMERO & ", " & _
                    " ""A"", " & _
                    " ""P"", " & _
                    " """ & general.NS(detallePAF.dvForm(i).Item("COLOR")) & """, " & _
                    " """ & nlineas + i + 1 & """," & _
                    " """ & nzn(detallePAF.dvForm(i).Item("TALL1"), 0) & """," & _
                    " """ & nzn(detallePAF.dvForm(i).Item("TALL2"), 0) & """," & _
                    " """ & nzn(detallePAF.dvForm(i).Item("TALL3"), 0) & """," & _
                    " """ & nzn(detallePAF.dvForm(i).Item("TALL4"), 0) & """," & _
                    " """ & nzn(detallePAF.dvForm(i).Item("TALL5"), 0) & """," & _
                    " """ & nzn(detallePAF.dvForm(i).Item("TALL6"), 0) & """," & _
                    " """ & nzn(detallePAF.dvForm(i).Item("TALL7"), 0) & """," & _
                    " """ & nzn(detallePAF.dvForm(i).Item("TALL8"), 0) & """," & _
                    " """ & nzn(detallePAF.dvForm(i).Item("TALL9"), 0) & """," & _
                    " """ & nzn(detallePAF.dvForm(i).Item("TALL10"), 0) & """," & _
                    " """ & general.NS(ObtenerTara(TALLA01), True) & """," & _
                    " """ & general.NS(ObtenerTara(TALLA02), True) & """," & _
                    " """ & general.NS(ObtenerTara(TALLA03), True) & """," & _
                    " """ & general.NS(ObtenerTara(TALLA04), True) & """," & _
                    " """ & general.NS(ObtenerTara(TALLA05), True) & """," & _
                    " """ & general.NS(ObtenerTara(TALLA06), True) & """," & _
                    " """ & general.NS(ObtenerTara(TALLA07), True) & """," & _
                    " """ & general.NS(ObtenerTara(TALLA08), True) & """," & _
                    " """ & general.NS(ObtenerTara(TALLA09), True) & """," & _
                    " """ & general.NS(ObtenerTara(TALLA10), True) & """," & _
                    " ""0"", " & _
                    " """ & general.NS(nzn(PREU * detallePAF.dvForm(i).Item("QUANTITAT"), 0), True) & """, " & _
                    " """ & centro & """, """ & unidadesTotales & """ ) "

                cmdIns.CommandText = SQL
                cmdIns.ExecuteNonQuery()
            Next
            CCN()

            Dim pafp As New clsPAFVenta(ds.Tables(tablaVentas).Copy, centro, Me.bc, Albaran, Prenda, False, NUMERO, False)
            pafp.lineasVenta.HacerCalculosLineasPAF("PREU", True)
            pafp.ActualizarOrigen()
            pafp = Nothing

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Sub Traspasar(ByVal aAlbaran As Double, ByVal aFecha As Date)
        Try
            'Primero obtener el número del último albarán
            'Esta funcion ha de trasapasar al orden actual al albarán correspondiente
            'Hay que mirar en anul las anulaciones de cada linea para traspasr solo lo
            'no anulado
            'No hay intervalos de traspaso, solo a albarán y la fecha
            If ALB = False Then
                If ComprobarAlbaranATraspasar(aAlbaran, aFecha) <> -1 Then
                    'Ya ha creado el nuevo o ha comprobado el de destino correcto
                    TraspasarAAlbaran(aAlbaran)
                    ALB = True
                    MessageBox.Show(rm.GetString("ORDENTRASPASADOOKALBARAN") & aAlbaran, rm.GetString("INFORMACION"), MessageBoxButtons.OK)
                End If
            Else
                MessageBox.Show(rm.GetString("ORDENYATRASPASADO"), rm.GetString("INFORMACION"), MessageBoxButtons.OK)
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "CONSUMOS"

    Public Sub GenerarConsumos(ByVal Ordenes As ArrayList)
        Dim iorden As Double

        Try
            IniciarTablaConsumos()
            For iorden = 0 To Ordenes.Count - 1
                CrearConsumosOrden(Ordenes(iorden))
            Next iorden

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub IniciarTablaConsumos()
        Dim dcTEJIDOC As New DataColumn("TEJIDO")
        Dim dcCOLORC As New DataColumn("COLOR")
        Dim dcCANTIDADC As New DataColumn("CANTIDAD")
        Dim dcACTUAL As New DataColumn("ACTUAL")
        Dim dcMINIMO As New DataColumn("MINIMO")
        Dim dcDIFERENCIA As New DataColumn("DIFERENCIA")
        Dim dcPRECIO As New DataColumn("PRECIO")
        Dim dcCONSUMO As New DataColumn("CONSUMO") 'Esto lo pongo para no complicarme la vida a la hora de ver
        'tjeidos que sean del mismo color y que sea el mismo tejido pero diferentes consumos
        Try
            If Not dtConsumos.Columns.Contains("TEJIDO") Then
                With dtConsumos.Columns
                    .Add(dcTEJIDOC)
                    .Add(dcCOLORC)
                    .Add(dcCANTIDADC)
                    .Add(dcACTUAL)
                    .Add(dcMINIMO)
                    .Add(dcDIFERENCIA)
                    .Add(dcPRECIO)
                    .Add(dcCONSUMO)
                End With
            Else
                dtConsumos.Rows.Clear()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerConsumoFornituras()
        Try

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub AgruparConsumosIguales()
        Dim dv As DataView = dtConsumos.DefaultView
        Dim i As Integer
        Try
            For i = 0 To dv.Count - 1

            Next
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerConsumoTejidos()
        Dim i As Integer
        Try

            'Para poner los que sean del mismo color y tejido pero vengan de consumos diferentes
            AgruparConsumosIguales()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub AñadirConsumosColor(ByVal orden As clsOrdenModelo, ByVal pos As Integer)
        Dim dvconsumo As Object = dtConsumos.DefaultView
        Dim cantidadColor, consumoColor As Double
        Dim cmdSelConsumo As MySqlCommand
        Dim COLORMODELO As String = orden.detallePAF.dvForm(pos).Item("COLOR")
        Dim tablaC As New DataTable
        Dim dss As New MySqlDataAdapter
        Try
            cantidadColor = nzn(orden.detallePAF.dvForm(pos).Item("TALL1"), 0) + nzn(orden.detallePAF.dvForm(pos).Item("TALL2"), 0) + _
                nzn(orden.detallePAF.dvForm(pos).Item("TALL3"), 0) + nzn(orden.detallePAF.dvForm(pos).Item("TALL4"), 0) + _
                nzn(orden.detallePAF.dvForm(pos).Item("TALL5"), 0) + nzn(orden.detallePAF.dvForm(pos).Item("TALL6"), 0) + _
                nzn(orden.detallePAF.dvForm(pos).Item("TALL7"), 0) + nzn(orden.detallePAF.dvForm(pos).Item("TALL8"), 0) + _
                nzn(orden.detallePAF.dvForm(pos).Item("TALL9"), 0) + nzn(orden.detallePAF.dvForm(pos).Item("TALL10"), 0)

            cmdSelConsumo = New MySqlCommand("SELECT " & _
                " modcol.coltitulo, " & _
                " MODELSESCANDALLO.COST, " & _
                " MODELSESCANDALLO.TEIXIT, " & _
                " MODELSESCANDALLO.CONSUM, " & _
                " modcol.modcol " & _
                " FROM modcol " & _
                " LEFT JOIN MODELSESCANDALLO ON (MODELSESCANDALLO.MODEL = modcol.MODEL   " & _
                " AND MODELSESCANDALLO.SERIE =  modcol.SERIE " & _
                " AND MODELSESCANDALLO.CLIENT = modcol.CLIENT " & _
                " AND MODELSESCANDALLO.TEMPORADA = modcol.TEMPORADA ) " & _
                " WHERE  modcol.MODEL = """ & general.NS(orden.MODEL) & """  AND  " & _
                " modcol.SERIE = """ & general.NS(orden.SERIE) & """  AND " & _
                " modcol.CLIENT = """ & general.NS(orden.CLIENT) & """  AND  " & _
                " modcol.TEMPORADA = """ & general.NS(orden.TEMPORADA) & """ AND " & _
                " modcol.MODCOL = """ & general.NS(COLORMODELO) & """  AND " & _
                " MODELSESCANDALLO.TITULO = modcol.TITULO", cnn)

            ACN()
            dss.SelectCommand = cmdSelConsumo
            'Dim lector As MySqlDataReader
            'lector = cmdSelConsumo.ExecuteReader
            dss.Fill(tablaC)


            'While lector.Read
            'Esta funcion añade el tejido su consumo en cantidad necesaria a la tabla'
            'dtconsumos o si existe ya lo añade la cantidad de ese tejido
            Dim i As Integer
            For i = 0 To tablaC.Rows.Count - 1
                ActualizarConsumoTejido(tablaC.Rows(i).Item("TEIXIT"), tablaC.Rows(i).Item("COST"), cantidadColor, COLORMODELO, tablaC.Rows(i).Item("coltitulo"), tablaC.Rows(i).Item("CONSUM"))
            Next

            'End While
            'lector.Close() : lector = Nothing
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub AñadirTejidoConsumo(ByVal tejidoAActualizar As String, ByVal COSTE As Double, ByVal cantidad As Double, ByVal COLORMODELO As String, ByVal coltitulo As String, ByVal CONSUM As Double)
        Dim dar2 As MySqlDataReader
        Dim cn2 As MySqlConnection = cnn '.Clone
        Dim cmdSelMinimoActual As New MySqlCommand
        Dim sqlSTR As String
        Dim i As Integer
        Dim dr As DataRow
        Try
            cmdSelMinimoActual.Connection = cn2
            '"CANTIDAD" es la cantidad del tejido y cantidad es la cantidad del color del modelo

            '*****************OBTENEMOS EL MINIMO Y EL ACTUAL DEL TEJIDO
            'para obtener el minimo y el actual tenemos que saber a que color corresponde el tejido
            'que estamos mirando para el modelo actual
            sqlSTR = "SELECT MINIM, ACTUAL FROM FILCOL WHERE FIL = """ & general.NS(tejidoAActualizar) & """ AND CENTRO = """ & centro & """ AND TIPUS = ""T"" AND COLOR = """
            cn2.Open()
            cmdSelMinimoActual.CommandText = sqlSTR & coltitulo & """"
            'cmdSelMinimoActual.FetchAll = True
            dar2 = cmdSelMinimoActual.ExecuteReader
            dar2.Read()
            '*****************FIN OBTENEMOS EL MINIMO Y EL ACTUAL DEL TEJIDO
            'REVISAR ERP
            If dar2.HasRows = False Then '.RecordCount = 0 Then
                MessageBox.Show("El color " & coltitulo & " del teixit " & tejidoAActualizar & " no existeix. S'ha d'introudir als colors del teixit per poder calcular el correctament consum del Model", "Informació", MessageBoxButtons.OK)
                Exit Sub
            End If
            dr = dtConsumos.NewRow
            dr("TEJIDO") = tejidoAActualizar
            dr("COLOR") = coltitulo

            dr("CANTIDAD") = cantidad * CONSUM
            dr("MINIMO") = nzn(dar2("MINIM"), 0)
            dr("ACTUAL") = nzn(dar2("ACTUAL"), 0)
            If dr("CANTIDAD") > dr("ACTUAL") Then
                dr("DIFERENCIA") = dr("CANTIDAD") - dr("ACTUAL")
            Else
                dr("DIFERENCIA") = 0
            End If
            dr("PRECIO") = roundnum(COSTE * cantidad, 2) '* dr("DIFERENCIA"), 2)

            dar2.Close()
            cn2.Close()
            dtConsumos.Rows.Add(dr)

            Try
                dar2.Close()
                CCN()
                If cn2.State = ConnectionState.Open Then cn2.Close()
            Catch ex As Exception

            End Try

        Catch ex As Exception
            LOG(ex.ToString) : CCN() : cn2.Close() : dar2.Close()
        End Try

    End Sub
    Private Sub ActualizarConsumoTejido(ByVal tejidoAActualizar As String, ByVal COSTE As Double, ByVal cantidad As Double, ByVal COLORMODELO As String, ByVal coltitulo As String, ByVal CONSUM As Double)
        'El coste este que le pasamos seguramente no vale para nada
        Dim dv As DataView = dtConsumos.DefaultView
        Dim idx As Integer
        Dim key(1) As String
        Try
            If Not coltitulo = "" Then
                dv.Sort = "TEJIDO, COLOR"

                key(0) = tejidoAActualizar
                key(1) = coltitulo
                idx = dv.Find(key)
                If idx = -1 Then
                    'En este caso no existe el tejido/color lo añadimos
                    'quiere decir que tenemos q añadirlo al dtconsumos
                    AñadirTejidoConsumo(tejidoAActualizar, COSTE, cantidad, COLORMODELO, coltitulo, CONSUM)
                Else
                    dv(idx).Item("CANTIDAD") = dv(idx).Item("CANTIDAD") + (cantidad * CONSUM) 'aColores(1)(i)
                    dv(idx).Item("PRECIO") = dv(idx).Item("PRECIO") + (COSTE * cantidad) 'dv(i).Item("DIFERENCIA")
                End If
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
        End Try

    End Sub
    Private Sub CrearConsumosOrden(ByVal orden As Double)
        Dim i As Integer
        Try
            'Cargamos la orden
            Dim ordenActual As New clsOrdenModelo(ds.Tables("cordre").Copy, centro, bc, "O", True, orden)

            For i = 0 To ordenActual.detallePAF.dvForm.Count - 1
                AñadirConsumosColor(ordenActual, i)
            Next

            PonerConsumoFornituras()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Sub GenerarPedidosCompra()
        Dim pedidoCompra As clsPAFCompra
        Dim ultimoTejido As String = ""
        Dim pedidosGenerados As String
        Dim i As Integer
        Try
            If dtConsumos.DefaultView.Count <> 0 Then
                Dim f As frmProveedoresLista = frmProveedoresLista.GetInstance(esEleccion)
                dtConsumos.DefaultView.Sort = "TEJIDO, COLOR"
                Try
                    ultimoTejido = dtConsumos.DefaultView(0).Item("TEJIDO")
                    MessageBox.Show("Selecciona un proveïdor per al teixit " & dtConsumos.DefaultView(i).Item("TEJIDO"))
                    f.ShowDialog()
                    If Not f.dr Is Nothing Then
                        pedidoCompra = New clsPAFCompra(ds.Tables("cactur").Copy, centro, bc, Pedido, Tejido, True, True, f.dr("CODI"))
                    End If
                    pedidosGenerados = pedidoCompra.FRA.ToString
                Catch ex As Exception : End Try

                For i = 0 To dtConsumos.DefaultView.Count - 1
                    If ultimoTejido <> dtConsumos.DefaultView(i).Item("TEJIDO") Then
                        'Hay que crear un nuevo pedido
                        MessageBox.Show("Selecciona un proveïdor per al teixit " & dtConsumos.DefaultView(i).Item("TEJIDO"))
                        f.ShowDialog()
                        If Not f.dr Is Nothing Then
                            pedidoCompra = New clsPAFCompra(ds.Tables("cactur").Copy, centro, bc, Pedido, Tejido, True, True, f.dr("CODI"))
                        End If
                        pedidosGenerados = pedidosGenerados & "," & pedidoCompra.FRA
                    End If
                    pedidoCompra.lineasCompra.AñadirLinea(dtConsumos.DefaultView(i).Item("TEJIDO"), dtConsumos.DefaultView(i).Item("COLOR"), dtConsumos.DefaultView(i).Item("CANTIDAD"), "M", nzn(DLookUp("PREUPERMODEL", "TEIXITS", "CODI = """ & general.NS(dtConsumos.DefaultView(i).Item("TEJIDO")) & """ AND CENTRO = """ & centro & """ "), 0), True)

                    ultimoTejido = dtConsumos.DefaultView(i).Item("TEJIDO")
                Next
                MessageBox.Show("S'han generat correctament les comandes de teixits següents: " & pedidosGenerados)
                f = Nothing
            End If

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

#End Region

End Class

'Private Sub ActualizarConsumoTejido(ByVal tejidoAActualizar As String, ByVal COSTE As Double, ByVal cantidad As Double, ByVal COLORMODELO As String, ByVal coltitulo As String, ByVal CONSUM As Double)
'    'El coste este que le pasamos seguramente no vale para nada
'    Dim dv As DataView = dtConsumos.DefaultView
'    Dim dr As DataRow
'    Dim cmdSelMinimoActual As New MySqlCommand
'    Dim dar2 As MySqlDataReader
'    Dim cn2 As MySqlConnection = cnn.Clone
'    'Dim aColores(1) As ArrayList
'    Dim sqlSTR As String
'    Dim i As Integer
'    Try
'        If Not coltitulo = "" Then
'            dv.Sort = "TEJIDO"
'            If dv.Find(tejidoAActualizar) = -1 Then
'                'quiere decir que tenemos q añadirlo al dtconsumos
'                AñadirTejidoConsumo(tejidoAActualizar, COSTE, cantidad, COLORMODELO, coltitulo, CONSUM)

'            Else
'                'aqui estamos en el caso de ya está añadido este tejido pero no sabemos si los colores que
'                'vamos a necesitar si lo estan
'                'primero obtenemos los colores
'                dv.Sort = "TEJIDO, COLOR"
'                Dim key(1) As String
'                key(0) = tejidoAActualizar

'                cmdSelMinimoActual.Connection = cn2
'                'para obtener el minimo y el actual tenemos que saber a que color corresponde el tejido
'                'que estamos mirando para el modelo actual
'                sqlSTR = "SELECT MINIM, ACTUAL FROM FILCOL WHERE FIL = """ & general.NS(tejidoAActualizar) & """ AND TIPUS = ""T"" AND COLOR = """
'                cn2.Open()
'                'For i = 0 To aColores(0).Count - 1
'                key(1) = coltitulo
'                If dv.Find(key) = -1 Then
'                    AñadirTejidoConsumo(tejidoAActualizar, COSTE, cantidad, COLORMODELO, coltitulo, CONSUM)
'                    'quiere decir que aunque tenemos un tejido del mimsmo tipo el color no está añadido
'                    'por lo que tenemos que actuar igual q en el caso de que no este añadido el tejido

'                    'cmdSelMinimoActual.CommandText = sqlSTR & coltitulo & """" 'aColores(0)(i) & """"
'                    'dar2 = cmdSelMinimoActual.ExecuteReader
'                    'dar2.Read()
'                    'dr = dtConsumos.NewRow

'                    'dr("TEJIDO") = tejidoAActualizar
'                    'dr("COLOR") = coltitulo
'                    ''"CANTIDAD" es la cantidad del tejido y cantidad es la cantidad del color del modelo
'                    'dr("CANTIDAD") = cantidad * CONSUM
'                    'dr("MINIMO") = nzn(dar2("MINIM"), 0)
'                    'dr("ACTUAL") = nzn(dar2("ACTUAL"), 0)
'                    'If dr("CANTIDAD") > dr("ACTUAL") Then
'                    '    dr("DIFERENCIA") = dr("CANTIDAD") - dr("ACTUAL")
'                    'Else
'                    '    dr("DIFERENCIA") = 0
'                    'End If

'                    'dr("PRECIO") = COSTE * cantidad '* dr("DIFERENCIA")
'                    'dar2.Close()
'                    'dtConsumos.Rows.Add(dr)
'                Else
'                    'Aunque los consumos sean diferentes no nos interesa solo añadir el tejido/color
'                    'dv.Sort = "TEJIDO, COLOR, CONSUMO"
'                    'Dim key1(2) As String
'                    'key1(0) = tejidoAActualizar
'                    'key1(1) = coltitulo
'                    'key1(2) = CONSUM
'                    'If dv.Find(key1) = -1 Then

'                    '    'nuevo consumo
'                    '    'quiere decir que aunque tenemos un tejido del mimsmo tipo el color no está añadido
'                    '    'por lo que tenemos que actuar igual q en el caso de que no este añadido el tejido
'                    '    cmdSelMinimoActual.CommandText = sqlSTR & coltitulo & """" 'aColores(0)(i) & """"
'                    '    dar2 = cmdSelMinimoActual.ExecuteReader
'                    '    dar2.Read()
'                    '    dr = dtConsumos.NewRow

'                    '    dr("TEJIDO") = tejidoAActualizar
'                    '    dr("COLOR") = coltitulo
'                    '    '"CANTIDAD" es la cantidad del tejido y cantidad es la cantidad del color del modelo
'                    '    dr("CANTIDAD") = cantidad * CONSUM
'                    '    dr("MINIMO") = nzn(dar2("MINIM"), 0)
'                    '    dr("ACTUAL") = nzn(dar2("ACTUAL"), 0)
'                    '    If dr("CANTIDAD") > dr("ACTUAL") Then
'                    '        dr("DIFERENCIA") = dr("CANTIDAD") - dr("ACTUAL")
'                    '    Else
'                    '        dr("DIFERENCIA") = 0
'                    '    End If
'                    '    dr("PRECIO") = COSTE * dr("DIFERENCIA")
'                    '    dar2.Close()
'                    '    dtConsumos.Rows.Add(dr)
'                    'Else

'                    'Aqui es el único caso que hemos de modificar el dv cuando ya existe el tejido/color

'                    dv(i).Item("CANTIDAD") = dv(i).Item("CANTIDAD") + cantidad * CONSUM 'aColores(1)(i)
'                    'If dv(i).Item("CANTIDAD") > dr("ACTUAL") Then
'                    'dv(i).Item("DIFERENCIA") = dv(i).Item("CANTIDAD") - dv(i).Item("ACTUAL")
'                    'Else
'                    '    dv(i).Item("DIFERENCIA") = 0
'                    'End If
'                    dv(i).Item("PRECIO") = dv(i).Item("PRECIO") + (COSTE * cantidad) 'dv(i).Item("DIFERENCIA")
'                    'End If
'                    cn2.Close()
'                End If
'            End If
'        End If
'        Try
'            dar2.Close()
'            CCN()
'            If cn2.State = ConnectionState.Open Then cn2.Close()

'        Catch ex As Exception

'        End Try
'    Catch ex As Exception
'        LOG(ex.ToString) : cargando = False : CCN() : cn2.Close() : dar2.Close()
'    End Try

'End Sub