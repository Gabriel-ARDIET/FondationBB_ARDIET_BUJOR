/*==============================================================*/
/* Table : EMPLOYE (Pour la gestion des rôles via login DB)     */
/*==============================================================*/
create table EMPLOYE (
   ID_EMPLOYE           SERIAL               not null,
   NOM_EMPLOYE          VARCHAR(100)         not null,
   PRENOM_EMPLOYE       VARCHAR(100)         not null,
   LOGIN_EMPLOYE           VARCHAR(50)          not null unique, -- Doit correspondre au USER Postgres
   ROLE_EMPLOYE         VARCHAR(20)          not null check (ROLE_EMPLOYE IN ('Bénévole', 'Responsable')),
   constraint PK_EMPLOYE primary key (ID_EMPLOYE)
);

/*==============================================================*/
/* Table : ESPECE                                               */
/*==============================================================*/
create table ESPECE (
   ID_ESPECE            SERIAL               not null,
   LIBELLE_ESPECE       VARCHAR(30)          not null,
   constraint PK_ESPECE primary key (ID_ESPECE)
);

/*==============================================================*/
/* Table : RACE                                                 */
/*==============================================================*/
create table RACE (
   ID_RACE              SERIAL               not null,
   ID_ESPECE            INT4                 not null,
   LIBELLE_RACE         VARCHAR(30)          not null,
   TAILLE_RACE          VARCHAR(30)          not null,
   constraint PK_RACE primary key (ID_RACE),
   constraint FK_RACE_ESPECE foreign key (ID_ESPECE) references ESPECE (ID_ESPECE)
);

/*==============================================================*/
/* Table : STATUT                                               */
/*==============================================================*/
create table STATUT (
   ID_STATUT            SERIAL               not null,
   LIBELLE_STATUT       VARCHAR(30)          not null,
   constraint PK_STATUT primary key (ID_STATUT)
);

/*==============================================================*/
/* Table : ETAT                                                 */
/*==============================================================*/
create table ETAT (
   ID_ETAT              SERIAL               not null,
   LIBELLE_ETAT         VARCHAR(30)          not null,
   constraint PK_ETAT primary key (ID_ETAT)
);

/*==============================================================*/
/* Table : PERSONNE                                             */
/*==============================================================*/
create table PERSONNE (
   ID_PERSONNE          SERIAL               not null,
   NOM_PERSONNE         VARCHAR(100)         not null,
   PRENOM_PERSONNE      VARCHAR(100)         not null,
   DATE_NAISSANCE_PERSONNE DATE                 null,
   NUMERO_PERSONNE      VARCHAR(10)          not null,
   RUE_PERSONNE         VARCHAR(100)         not null,
   CP_PERSONNE          CHAR(5)              not null,
   VILLE_PERSONNE       VARCHAR(50)          not null,
   TELEPHONE_PERSONNE   CHAR(10)             not null,
   MAIL_PERSONNE        VARCHAR(100)         null,
   DATE_CREATION_PERSONNE DATE               not null default CURRENT_DATE,
   constraint PK_PERSONNE primary key (ID_PERSONNE),
   constraint UK_PERSONNE_TEL unique (TELEPHONE_PERSONNE) -- Contrainte d'unicité demandée
);

/*==============================================================*/
/* Table : ADOPTION                                             */
/*==============================================================*/
create table ADOPTION (
   ID_ADOPTION          SERIAL               not null,
   ID_PERSONNE          INT4                 not null,
   ID_ANIMAL            INT4                 not null,
   ID_EMPLOYE           INT4                 not null, -- Qui a fait signer le contrat ?
   FRAIS_ADOPTION       DECIMAL(8,2)         not null check (FRAIS_ADOPTION >= 0),
   DATE_ADOPTION        DATE                 not null default CURRENT_DATE,
   constraint PK_ADOPTION primary key (ID_ADOPTION),
   constraint FK_ADOPTION_PERSONNE foreign key (ID_PERSONNE) references PERSONNE (ID_PERSONNE),
   constraint FK_ADOPTION_EMPLOYE foreign key (ID_EMPLOYE) references EMPLOYE (ID_EMPLOYE)
);

/*==============================================================*/
/* Table : ANIMAL                                               */
/*==============================================================*/
create table ANIMAL (
   ID_ANIMAL            SERIAL               not null,
   ID_STATUT            INT4                 null,
   ID_RACE              INT4                 not null,
   ID_ETAT              INT4                 null,
   ID_ADOPTION          INT4                 null,
   ID_EMPLOYE           INT4                 null, -- Qui a enregistré l'animal ?
   NOM_ANIMAL           VARCHAR(50)          not null,
   DATE_NAISSANCE_ANIMAL DATE                 null,
   I_CAD_ANIMAL         CHAR(15)             null,
   SEXE_ANIMAL          CHAR(1)              not null check (SEXE_ANIMAL IN ('M', 'F')),
   ANNOTATION_ANIMAL    TEXT                 null,
   DATE_ARRIVEE_ANIMAL  DATE                 not null default CURRENT_DATE,
   POIDS_ANIMAL         NUMERIC(5,2)         not null check (POIDS_ANIMAL > 0),
   constraint PK_ANIMAL primary key (ID_ANIMAL),
   constraint UK_I_CAD unique (I_CAD_ANIMAL),
   constraint FK_ANIMAL_STATUT foreign key (ID_STATUT) references STATUT (ID_STATUT),
   constraint FK_ANIMAL_RACE foreign key (ID_RACE) references RACE (ID_RACE),
   constraint FK_ANIMAL_ETAT foreign key (ID_ETAT) references ETAT (ID_ETAT),
   constraint FK_ANIMAL_ADOPTION foreign key (ID_ADOPTION) references ADOPTION (ID_ADOPTION),
   constraint FK_ANIMAL_EMPLOYE foreign key (ID_EMPLOYE) references EMPLOYE (ID_EMPLOYE)
);

alter table ADOPTION add constraint FK_ADOPTION_ANIMAL foreign key (ID_ANIMAL) references ANIMAL (ID_ANIMAL);

/*==============================================================*/
/* Tables suivantes (COMPORTEMENT, SOIN, RECOIT, DEMANDE)        */
/*==============================================================*/

create table COMPORTEMENT (
   ID_COMPORTEMENT      SERIAL               not null,
   LIBELLE_COMPORTEMENT VARCHAR(30)          not null,
   constraint PK_COMPORTEMENT primary key (ID_COMPORTEMENT)
);

create table ANIMAL_COMPORTEMENT (
   ID_COMPORTEMENT      INT4                 not null,
   ID_ANIMAL            INT4                 not null,
   constraint PK_ANIMAL_COMPORTEMENT primary key (ID_COMPORTEMENT, ID_ANIMAL),
   constraint FK_AC_COMPORTEMENT foreign key (ID_COMPORTEMENT) references COMPORTEMENT (ID_COMPORTEMENT) on delete cascade,
   constraint FK_AC_ANIMAL foreign key (ID_ANIMAL) references ANIMAL (ID_ANIMAL) on delete cascade
);

create table DEMANDE (
   ID_DEMANDE           SERIAL               not null,
   ID_RACE              INT4                 null,
   ID_PERSONNE          INT4                 not null,
   DATE_DEMANDE         DATE                 not null default CURRENT_DATE,
   TRANCHE_AGE_DEMANDE  VARCHAR(100)         null,
   constraint PK_DEMANDE primary key (ID_DEMANDE),
   constraint FK_DEMANDE_PERSONNE foreign key (ID_PERSONNE) references PERSONNE (ID_PERSONNE),
   constraint FK_DEMANDE_RACE foreign key (ID_RACE) references RACE (ID_RACE)
);

create table SOIN (
   ID_SOIN              SERIAL               not null,
   LIBELLE_SOIN         VARCHAR(50)          not null,
   TARIF_SOIN           DECIMAL(8,2)         null check (TARIF_SOIN >= 0),
   FREQUENCE_SOIN       INT4                 null check (FREQUENCE_SOIN >= 0),
   constraint PK_SOIN primary key (ID_SOIN)
);

create table RECOIT (
   ID_SOIN              INT4                 not null,
   ID_ANIMAL            INT4                 not null,
   DATE_SOIN            DATE                 not null,
   DATE_RAPPEL          DATE                 null,
   constraint PK_RECOIT primary key (ID_SOIN, ID_ANIMAL, DATE_SOIN),
   constraint FK_RECOIT_SOIN foreign key (ID_SOIN) references SOIN (ID_SOIN),
   constraint FK_RECOIT_ANIMAL foreign key (ID_ANIMAL) references ANIMAL (ID_ANIMAL)
);