from recommonmark.parser import CommonMarkParser

source_parsers = {
    '.md': CommonMarkParser,
}

source_suffix = ['.md']

def setup(app):
    app.add_stylesheet('https://media.readthedocs.org/css/sphinx_rtd_theme.css')
