using System.ComponentModel;

namespace Esmiylara.Enumerations
{
    /// <summary>
    /// Defines verbosity values.
    /// </summary>
    public enum Verbosity
    {
        [Description("None")]
        None = (1 << 0),

        [Description("Error")]
        Error = (1 << 1),

        [Description("Warning")]
        Warning = (1 << 2),

        [Description("Info")]
        Info = (1 << 3),

        [Description("Debug")]
        Debug = (1 << 4),

        [Description("Verbose")]
        Verbose = (1 << 5),

        [Description("All")]
        All = ~0,

        /* Aliases */
        Default = Info,

        Production = Error,

        Developer = Verbose
    }
}