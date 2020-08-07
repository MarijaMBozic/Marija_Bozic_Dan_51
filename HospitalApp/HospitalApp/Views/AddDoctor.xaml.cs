﻿using HospitalApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HospitalApp.Views
{
    /// <summary>
    /// Interaction logic for AddDoctor.xaml
    /// </summary>
    public partial class AddDoctor : Window
    {
        public AddDoctor()
        {
            InitializeComponent();
            this.DataContext = new AddDoctorViewModels(this);
        }

        public AddDoctor(vwDoctor doctorEdit)
        {
            InitializeComponent();
            this.DataContext = new AddDoctorViewModels(this, doctorEdit);
        }
    }
}
