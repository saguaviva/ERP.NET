Public Class frmSplash
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmSplash))
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AccessibleDescription = resources.GetString("Label1.AccessibleDescription")
        Me.Label1.AccessibleName = resources.GetString("Label1.AccessibleName")
        Me.Label1.Anchor = CType(resources.GetObject("Label1.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = CType(resources.GetObject("Label1.AutoSize"), Boolean)
        Me.Label1.Dock = CType(resources.GetObject("Label1.Dock"), System.Windows.Forms.DockStyle)
        Me.Label1.Enabled = CType(resources.GetObject("Label1.Enabled"), Boolean)
        Me.Label1.Font = CType(resources.GetObject("Label1.Font"), System.Drawing.Font)
        Me.Label1.Image = CType(resources.GetObject("Label1.Image"), System.Drawing.Image)
        Me.Label1.ImageAlign = CType(resources.GetObject("Label1.ImageAlign"), System.Drawing.ContentAlignment)
        Me.Label1.ImageIndex = CType(resources.GetObject("Label1.ImageIndex"), Integer)
        Me.Label1.ImeMode = CType(resources.GetObject("Label1.ImeMode"), System.Windows.Forms.ImeMode)
        Me.Label1.Location = CType(resources.GetObject("Label1.Location"), System.Drawing.Point)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = CType(resources.GetObject("Label1.RightToLeft"), System.Windows.Forms.RightToLeft)
        Me.Label1.Size = CType(resources.GetObject("Label1.Size"), System.Drawing.Size)
        Me.Label1.TabIndex = CType(resources.GetObject("Label1.TabIndex"), Integer)
        Me.Label1.Text = resources.GetString("Label1.Text")
        Me.Label1.TextAlign = CType(resources.GetObject("Label1.TextAlign"), System.Drawing.ContentAlignment)
        Me.Label1.Visible = CType(resources.GetObject("Label1.Visible"), Boolean)
        '
        'frmSplash
        '
        Me.AccessibleDescription = resources.GetString("$this.AccessibleDescription")
        Me.AccessibleName = resources.GetString("$this.AccessibleName")
        Me.AutoScaleBaseSize = CType(resources.GetObject("$this.AutoScaleBaseSize"), System.Drawing.Size)
        Me.AutoScroll = CType(resources.GetObject("$this.AutoScroll"), Boolean)
        Me.AutoScrollMargin = CType(resources.GetObject("$this.AutoScrollMargin"), System.Drawing.Size)
        Me.AutoScrollMinSize = CType(resources.GetObject("$this.AutoScrollMinSize"), System.Drawing.Size)
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = CType(resources.GetObject("$this.ClientSize"), System.Drawing.Size)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label1)
        Me.Enabled = CType(resources.GetObject("$this.Enabled"), Boolean)
        Me.Font = CType(resources.GetObject("$this.Font"), System.Drawing.Font)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.ImeMode = CType(resources.GetObject("$this.ImeMode"), System.Windows.Forms.ImeMode)
        Me.Location = CType(resources.GetObject("$this.Location"), System.Drawing.Point)
        Me.MaximumSize = CType(resources.GetObject("$this.MaximumSize"), System.Drawing.Size)
        Me.MinimumSize = CType(resources.GetObject("$this.MinimumSize"), System.Drawing.Size)
        Me.Name = "frmSplash"
        Me.RightToLeft = CType(resources.GetObject("$this.RightToLeft"), System.Windows.Forms.RightToLeft)
        Me.StartPosition = CType(resources.GetObject("$this.StartPosition"), System.Windows.Forms.FormStartPosition)
        Me.Text = resources.GetString("$this.Text")
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private T As Timer
    Private bOwnerFinishedLoading As Boolean

    'constructors
    'the default constructor has been removed from " Windows Form Designer generated code "
    'First constructor: only show the splash while the form is loading
    Public Sub New(ByVal Owner As Form)
        MyBase.New()
        Me.Owner = Owner
        'add an eventhandler for the activated event of the calling
        'form. If the load event was trapped, the splash screen
        'would not be visible while the loading of the calling
        'form takes place. (Activated event fires after the load
        'event has finished)
        AddHandler Owner.Activated, AddressOf Done
        'InitializeComponent is needed to initialize the splash forms controls
        InitializeComponent()
        Me.Show()
        'call doevents to ensure the splash is shown, before the
        'initialization of the main form continues
        Application.DoEvents()
    End Sub

    '2nd constructor: show the splash for a minimum amount of time.
    'If the form loading takes longer, the splash is shown while the
    'form is loading, but it's always at least shown the number of 
    'seconds given.
    Public Sub New(ByVal Owner As Form, ByVal TimeToShow As Integer)
        'call first constructor
        Me.New(Owner)
        'initialize timer object
        T = New Timer
        T.Interval = TimeToShow * 1000
        T.Enabled = True
        AddHandler T.Tick, AddressOf Done
        AddHandler Me.Owner.VisibleChanged, AddressOf HideOwner
    End Sub

    'this sub hides the main form, if a minimum time to
    'show the splash has been given and the loading is complete
    'before that time has elapsed
    'since hiding the main form would cause the application to exit,
    'the form is made transparent
    Private Sub HideOwner(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim F As Form = CType(sender, Form)
        F.Opacity = 0
    End Sub

    'this sub fires when the loading of the calling form is complete
    'If a minimum show time has been given, it also fires when that
    'time has elapsed
    Private Sub Done(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If sender Is Me.Owner Then
            'owner has finished loading
            'set flag
            bOwnerFinishedLoading = True
            'remove the handler to ensure this sub will not fire again 
            'for the activated event.
            RemoveHandler Owner.Activated, AddressOf Done
            'if there is no time set, or that time has elapsed already,
            'the splash can be closed
            If T Is Nothing Then
                Finish()
            End If
        ElseIf bOwnerFinishedLoading Then
            'timer ticked, owner has finised initializing
            Finish()
        Else
            'timer has ticked, but owner hasn't finished initialzing
            'yet, wait for owner to finish.
            'set the timer object to nothing, so when the owning form
            'has finished loading, it will know it can close the splash
            RemoveHandler Owner.VisibleChanged, AddressOf HideOwner
            T.Enabled = False
            T = Nothing
        End If
    End Sub

    'this sub is called when either the loading is completed or
    'the time has elapsed, depending on the settings
    'This method can also be called from the calling form to 
    'close the splash before these events occur
    Public Sub Finish()
        If Not T Is Nothing Then T.Dispose()
        If Owner.Opacity = 0 Then Owner.Opacity = 100
        Me.Close()
    End Sub


End Class
