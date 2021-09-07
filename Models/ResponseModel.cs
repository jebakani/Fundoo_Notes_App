// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResponseModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Jebakani Ishwarya"/>
// ----------------------------------------------------------------------------------------------------------

namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// The class that stores the response details
    /// </summary>
    /// <typeparam name="T">It can be any type</typeparam>
    public class ResponseModel<T>
    {
        /// <summary>
        /// Gets or sets a value indicating whether status as true or false
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the message as string
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a data as given type
        /// Data is optional whenever needed data is returned
        /// </summary>
        public T Data { get; set; } 
    }
}
