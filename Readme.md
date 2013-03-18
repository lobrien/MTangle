# C# Literate Programming in HTML

A command-line program, written in C#, that extracts chunks of code (well, chunks of any text)
from an HTML file and assembles them into a single string, which can then be written to 
disk for compilation. 

Typical usage: `mono mtangle.exe someFile.html ChunkName > myFile.cs`

The program does two simple things:

(1) It prints the contents of an HTML preformatted block of the form:

    <pre id="ChunkName">
    code
    </pre>

(2) Within such a preformatted "chunk", it replaces pseudo-tags of the form `&lt;getchunk id="innerChunk"&gt;`
with the chunk identified by the `id` pseudo-parameter. In other words:

    <pre id="ChunkName">
    code
    </pre>

    <pre id="ChunkName">
    preCode
    &lt;getchunk id="include"&gt;
    postCode
    </pre>
    
Would be retrieved as:

    preCode
    code
    postCode
    
---    

This is a port of the C program described at http://axiom-developer.org/axiom-website/litprog.html

Rather than really parsing the input file as either HTML or XML, this program uses hard-coded text 
and regular expressions. That's per the original C program; it would be trivial to change it to, 
at the very least, work with XHTML-style HTML. 

Ultimately, I'd like to do a Markdown-driven literate programming editor, but that's a major undertaking.