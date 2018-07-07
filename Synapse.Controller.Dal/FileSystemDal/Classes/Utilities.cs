using System.IO;

public class Utilities
{
    public static string PathCombine(params string[] paths)
    {
        if( paths.Length > 0 )
        {
            int last = paths.Length - 1;
            for( int c = 0; c <= last; c++ )
            {
                if( c != 0 )
                    paths[c] = paths[c].Trim( Path.DirectorySeparatorChar );

                if( c != last )
                    paths[c] = string.Format( "{0}\\", paths[c] );
            }
        }
        else
        {
            return string.Empty;
        }

        return Path.Combine( paths );
    }
}