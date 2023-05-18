CREATE DATABASE BABY_FOOT;
\c BABY_FOOT

CREATE TABLE JOUEUR(
    idJoueur Serial PRIMARY KEY,
    nom VARCHAR(255) NOT NULL
);

INSert into JOUEUR(nom) VALUES('Jean'),('Julien'),('Julie'),('Javier');

CREATE TABLE PROPRIO_BABY(
    idProprio Serial PRIMARY KEY ,
    nom VARCHAR(255) NOT NULL
);

INSert into PROPRIO_BABY(nom) VALUES('Baby_mania'),('bb');

CREATE TABLE MATCH(
    idMatch Serial PRIMARY KEY ,
    idJoueur1 INT REFERENCES JOUEUR(idJoueur),
    idJoueur2 INT REFERENCES JOUEUR(idJoueur),
    idProprio INT REFERENCES PROPRIO_BABY(idProprio),
    mise decimal default NULL
);

CREATE TABLE PORTE_FEUILLE_JOUEUR(
    idJoueur INT REFERENCES JOUEUR(idJoueur),
    idMatch INT REFERENCES MATCH(idMatch),
    argent float
);

CREATE TABLE PORTE_FEUILLE_PROPRIO(
    idProprio INT REFERENCES PROPRIO_BABY(idProprio),
    idMatch INT REFERENCES MATCH(idMatch),
    argent float
);

CREATE VIEW V_argent_par_joueur
    AS
        SELECT SUM(PFJ.argent) etat, PFJ.idMatch FROM JOUEUR J 
            JOIN PORTE_FEUILLE_JOUEUR PFJ 
                ON J.idJoueur = PFJ.idJoueur
            JOIN MATCH M
                ON M.idMatch = PFJ.idMatch