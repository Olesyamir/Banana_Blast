<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:savedGames="http://www.univ-grenoble-alpes.fr/l3miage/jeu_video"
                exclude-result-prefixes="savedGames">

    <!-- Configuration de la sortie HTML -->
    <xsl:output method="html" encoding="UTF-8" indent="yes"/>

    <!-- Modèle principal -->
    <xsl:template match="/savedGames:savedGames">
        <html>
            <head>
                <title>Hi-Scores - Space Shooter</title>
                <style>
                    body {
                    font-family: 'Orbitron', sans-serif;
                    margin: 0;
                    padding: 0;
                    background: linear-gradient(45deg, #1e1e2f, #2a2a48);
                    color: white;
                    text-align: center;
                    }
                    h1 {
                    font-size: 3em;
                    margin-top: 20px;
                    color: #ffcc00;
                    text-shadow: 2px 2px 10px #ff9900;
                    }
                    table {
                    margin: 30px auto;
                    width: 60%;
                    border-collapse: collapse;
                    box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.5);
                    }
                    th, td {
                    padding: 15px;
                    text-align: center;
                    font-size: 1.2em;
                    border: 1px solid rgba(255, 255, 255, 0.2);
                    }
                    th {
                    background-color: #4e4e70;
                    color: #ffcc00;
                    text-transform: uppercase;
                    }
                    td {
                    background: rgba(255, 255, 255, 0.1);
                    transition: background 0.3s;
                    }
                    td:hover {
                    background: rgba(255, 255, 255, 0.2);
                    }
                    tr:nth-child(even) td {
                    background: rgba(0, 0, 0, 0.3);
                    }
                    tr:nth-child(even) td:hover {
                    background: rgba(255, 255, 255, 0.2);
                    }
                </style>
            </head>
            <body>
                <!-- Titre principal -->
                <h1>Hi-Scores - Space Shooter</h1>

                <!-- Tableau des scores -->
                <table>
                    <tr>
                        <th>Nom du joueur</th>
                        <th>Score</th>
                    </tr>
                    <!-- Parcours des parties triées par score décroissant -->
                    <xsl:for-each select="savedGames:parties/savedGames:partie">
                        <xsl:sort select="savedGames:score" data-type="number" order="descending"/>
                        <tr>
                            <td><xsl:value-of select="savedGames:nomjoueur"/></td>
                            <td><xsl:value-of select="savedGames:score"/></td>
                        </tr>
                    </xsl:for-each>
                </table>
            </body>
        </html>
    </xsl:template>

</xsl:stylesheet>
