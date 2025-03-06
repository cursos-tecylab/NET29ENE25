�
`D:\Git\NET29ENE25\sesion13\docentes\src\Docentes\Docentes.Infrastructure\ApplicationDbContext.cs
	namespace 	
Docentes
 
. 
Infrastructure !
;! "
public 
class  
ApplicationDbContext !
:" #
	DbContext$ -
,- .
IUnitOfWork/ :
{		 
public

 

readonly

 

IPublisher

 

_publisher

 )
;

) *
public  
ApplicationDbContext  
(  !
DbContextOptions! 1
options2 9
,9 :

IPublisher; E
	publisherF O
)O P
:Q R
baseS W
(W X
optionsX _
)_ `
{ 

_publisher 
= 
	publisher 
; 
} 
public 

override 
async 
Task 
< 
int "
>" #
SaveChangesAsync$ 4
(4 5
CancellationToken5 F
cancellationTokenG X
)X Y
{ 
try 
{ 	
var 
result 
= 
await 
base #
.# $
SaveChangesAsync$ 4
(4 5
cancellationToken5 F
)F G
;G H
return 
result 
; 
} 	
catch 
( (
DbUpdateConcurrencyException +
ex, .
). /
{ 	
throw 
new  
ConcurrencyException *
(* +
$str+ U
,U V
exW Y
)Y Z
;Z [
} 	
} 
	protected 
override 
void 
OnModelCreating +
(+ ,
ModelBuilder, 8
modelBuilder9 E
)E F
{ 
modelBuilder   
.   +
ApplyConfigurationsFromAssembly   4
(  4 5
typeof  5 ;
(  ; < 
ApplicationDbContext  < P
)  P Q
.  Q R
Assembly  R Z
)  Z [
;  [ \
base!! 
.!! 
OnModelCreating!! 
(!! 
modelBuilder!! )
)!!) *
;!!* +
}"" 
}$$ �
yD:\Git\NET29ENE25\sesion13\docentes\src\Docentes\Docentes.Infrastructure\Configurations\CursosImpartidosConfigurations.cs
	namespace 	
Docentes
 
. 
Infrastructure !
.! "
Configurations" 0
;0 1
internal 
sealed	 
class *
CursosImpartidosConfigurations 4
:5 6$
IEntityTypeConfiguration7 O
<O P
CursoImpartidoP ^
>^ _
{ 
public		 

void		 
	Configure		 
(		 
EntityTypeBuilder		 +
<		+ ,
CursoImpartido		, :
>		: ;
builder		< C
)		C D
{

 
builder 
. 
ToTable 
( 
$str +
)+ ,
;, -
builder 
. 
HasKey 
( 
x 
=> 
x 
. 
Id  
)  !
;! "
builder 
. 
Property 
( 
docente  
=>! #
docente$ +
.+ ,
	DocenteId, 5
)5 6
;6 7
builder 
. 
Property 
( 
curso 
=> !
curso" '
.' (
CursoId( /
)/ 0
;0 1
builder 
. 
HasOne 
( 
docente 
=> !
docente" )
.) *
Docente* 1
)1 2
. 	
WithMany	 
( 
) 
. 	
HasForeignKey	 
( 
docente 
=> !
docente" )
.) *
	DocenteId* 3
)3 4
. 	

IsRequired	 
( 
) 
; 
builder 
. 
Property 
< 
uint 
> 
( 
$str (
)( )
.) *
IsRowVersion* 6
(6 7
)7 8
;8 9
} 
} �
pD:\Git\NET29ENE25\sesion13\docentes\src\Docentes\Docentes.Infrastructure\Configurations\DocenteConfigurations.cs
	namespace 	
Docentes
 
. 
Infrastructure !
.! "
Configurations" 0
;0 1
public 
class !
DocenteConfigurations "
:# $$
IEntityTypeConfiguration% =
<= >
Docente> E
>E F
{ 
public		 
void		 
	Configure		 
(		 
EntityTypeBuilder		 -
<		- .
Docente		. 5
>		5 6
builder		7 >
)		> ?
{

 
builder 
. 
ToTable 
( 
$str "
)" #
;# $
builder 
. 
HasKey 
( 
x 
=> 
x 
. 
Id  
)  !
;! "
builder 
. 
Property 
( 
usuario  
=>! #
usuario$ +
.+ ,
	UsuarioId, 5
)5 6
;6 7
builder 
. 
HasIndex 
( 
usuario  
=>! #
usuario$ +
.+ ,
	UsuarioId, 5
)5 6
.6 7
IsUnique7 ?
(? @
)@ A
;A B
builder 
. 
Property 
( 
especialidad %
=>& (
especialidad) 5
.5 6
EspecialidadId6 D
)D E
;E F
builder 
. 
Property 
< 
uint 
> 
( 
$str (
)( )
.) *
IsRowVersion* 6
(6 7
)7 8
;8 9
} 
} �%
_D:\Git\NET29ENE25\sesion13\docentes\src\Docentes\Docentes.Infrastructure\DependencyInjection.cs
	namespace 	
Docentes
 
. 
Infrastructure !
;! "
public 
static 
class 
DependencyInjection '
{ 
public 

static 
IServiceCollection $
AddInfrastructure% 6
(6 7
this 
IServiceCollection 
services '
,' (
IConfiguration 
configuration #
) 
{ 
var $
connectionStringPostgres $
=% &
configuration' 4
.4 5
GetConnectionString5 H
(H I
$strI S
)S T
?? 

throw 
new !
ArgumentNullException *
(* +
nameof+ 1
(1 2
configuration2 ?
)? @
)@ A
;A B
var !
connectionStringRedis !
=" #
configuration$ 1
.1 2
GetConnectionString2 E
(E F
$strF M
)M N
?? 

throw 
new !
ArgumentNullException *
(* +
nameof+ 1
(1 2
configuration2 ?
)? @
)@ A
;A B
var 
usuarioApiBaseUrl 
= 
configuration  -
[- .
$str. A
]A B
;B C
var 
cursoApiBaseUrl 
= 
configuration +
[+ ,
$str, >
]> ?
;? @
services 
. 
AddDbContext 
<  
ApplicationDbContext 2
>2 3
(3 4
options 
=> 
{ 
options   
.   
	UseNpgsql   !
(  ! "$
connectionStringPostgres  " :
)  : ;
.  ; <(
UseSnakeCaseNamingConvention  < X
(  X Y
)  Y Z
;  Z [
}!! 
)"" 	
;""	 

services$$ 
.$$ 
AddSingleton$$ 
<$$ "
IConnectionMultiplexer$$ 4
>$$4 5
($$5 6
sp$$7 9
=>$$: <
{%% 	
var&& 
configurationRedis&& "
=&&# $ 
ConfigurationOptions&&% 9
.&&9 :
Parse&&: ?
(&&? @!
connectionStringRedis&&@ U
)&&U V
;&&V W
return'' !
ConnectionMultiplexer'' (
.''( )
Connect'') 0
(''0 1
configurationRedis''1 C
)''C D
;''D E
}(( 	
)((	 

;((
 
services** 
.** 
	AddScoped** 
<** 
IDocenteRepository** -
,**- .
DocenteRepository**/ @
>**@ A
(**A B
)**B C
;**C D
services++ 
.++ 
	AddScoped++ 
<++ %
ICursoImpartidoRepository++ 4
,++4 5$
CursoImpartidoRepository++6 N
>++N O
(++O P
)++P Q
;++Q R
services,, 
.,, 
	AddScoped,, 
<,, 
IUnitOfWork,, &
>,,& '
(,,' (
sp,,( *
=>,,+ -
sp,,. 0
.,,0 1
GetRequiredService,,1 C
<,,C D 
ApplicationDbContext,,D X
>,,X Y
(,,Y Z
),,Z [
),,[ \
;,,\ ]
services.. 
... 
	AddScoped.. 
<.. 
ICacheService.. (
,..( )
CacheService..) 5
>..5 6
(..6 7
)..7 8
;..8 9
services00 
.00 
AddHttpClient00 
<00 
ICursosService00 -
,00- .
CursosService00. ;
>00; <
(00< =
cliente00> E
=>00F H
{11 	
cliente22 
.22 
BaseAddress22 
=22  !
new22" %
Uri22& )
(22) *
cursoApiBaseUrl22* 9
!229 :
)22: ;
;22; <
}33 	
)33	 

;33
 
services55
 
.55 
AddHttpClient55  
<55  !
IUsuarioService55! 0
,550 1
UsuarioService551 ?
>55? @
(55@ A
cliente55B I
=>55J L
{66 	
cliente77 
.77 
BaseAddress77 
=77  !
new77" %
Uri77& )
(77) *
usuarioApiBaseUrl77* ;
!77; <
)77< =
;77= >
}88 	
)88	 

;88
 
return:: 
services:: 
;:: 
};; 
}<< �0
sD:\Git\NET29ENE25\sesion13\docentes\src\Docentes\Docentes.Infrastructure\Migrations\20240625225153_InitialCreate.cs
	namespace 	
Docentes
 
. 
Infrastructure !
.! "

Migrations" ,
{ 
public		 

partial		 
class		 
InitialCreate		 &
:		' (
	Migration		) 2
{

 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{ 	
migrationBuilder 
. 
CreateTable (
(( )
name 
: 
$str  
,  !
columns 
: 
table 
=> !
new" %
{ 
id 
= 
table 
. 
Column %
<% &
Guid& *
>* +
(+ ,
type, 0
:0 1
$str2 8
,8 9
nullable: B
:B C
falseD I
)I J
,J K

usuario_id 
=  
table! &
.& '
Column' -
<- .
Guid. 2
>2 3
(3 4
type4 8
:8 9
$str: @
,@ A
nullableB J
:J K
falseL Q
)Q R
,R S
especialidad_id #
=$ %
table& +
.+ ,
Column, 2
<2 3
Guid3 7
>7 8
(8 9
type9 =
:= >
$str? E
,E F
nullableG O
:O P
falseQ V
)V W
,W X
xmin 
= 
table  
.  !
Column! '
<' (
uint( ,
>, -
(- .
type. 2
:2 3
$str4 9
,9 :

rowVersion; E
:E F
trueG K
,K L
nullableM U
:U V
falseW \
)\ ]
} 
, 
constraints 
: 
table "
=># %
{ 
table 
. 

PrimaryKey $
($ %
$str% 2
,2 3
x4 5
=>6 8
x9 :
.: ;
id; =
)= >
;> ?
} 
) 
; 
migrationBuilder 
. 
CreateTable (
(( )
name 
: 
$str )
,) *
columns 
: 
table 
=> !
new" %
{ 
id   
=   
table   
.   
Column   %
<  % &
Guid  & *
>  * +
(  + ,
type  , 0
:  0 1
$str  2 8
,  8 9
nullable  : B
:  B C
false  D I
)  I J
,  J K

docente_id!! 
=!!  
table!!! &
.!!& '
Column!!' -
<!!- .
Guid!!. 2
>!!2 3
(!!3 4
type!!4 8
:!!8 9
$str!!: @
,!!@ A
nullable!!B J
:!!J K
false!!L Q
)!!Q R
,!!R S
curso_id"" 
="" 
table"" $
.""$ %
Column""% +
<""+ ,
Guid"", 0
>""0 1
(""1 2
type""2 6
:""6 7
$str""8 >
,""> ?
nullable""@ H
:""H I
true""J N
)""N O
,""O P
xmin## 
=## 
table##  
.##  !
Column##! '
<##' (
uint##( ,
>##, -
(##- .
type##. 2
:##2 3
$str##4 9
,##9 :

rowVersion##; E
:##E F
true##G K
,##K L
nullable##M U
:##U V
false##W \
)##\ ]
}$$ 
,$$ 
constraints%% 
:%% 
table%% "
=>%%# %
{&& 
table'' 
.'' 

PrimaryKey'' $
(''$ %
$str''% ;
,''; <
x''= >
=>''? A
x''B C
.''C D
id''D F
)''F G
;''G H
table(( 
.(( 

ForeignKey(( $
((($ %
name)) 
:)) 
$str)) G
,))G H
column** 
:** 
x**  !
=>**" $
x**% &
.**& '

docente_id**' 1
,**1 2
principalTable++ &
:++& '
$str++( 2
,++2 3
principalColumn,, '
:,,' (
$str,,) -
,,,- .
onDelete--  
:--  !
ReferentialAction--" 3
.--3 4
Cascade--4 ;
)--; <
;--< =
}.. 
).. 
;.. 
migrationBuilder00 
.00 
CreateIndex00 (
(00( )
name11 
:11 
$str11 7
,117 8
table22 
:22 
$str22 *
,22* +
column33 
:33 
$str33 $
)33$ %
;33% &
migrationBuilder55 
.55 
CreateIndex55 (
(55( )
name66 
:66 
$str66 .
,66. /
table77 
:77 
$str77 !
,77! "
column88 
:88 
$str88 $
,88$ %
unique99 
:99 
true99 
)99 
;99 
}:: 	
	protected== 
override== 
void== 
Down==  $
(==$ %
MigrationBuilder==% 5
migrationBuilder==6 F
)==F G
{>> 	
migrationBuilder?? 
.?? 
	DropTable?? &
(??& '
name@@ 
:@@ 
$str@@ )
)@@) *
;@@* +
migrationBuilderBB 
.BB 
	DropTableBB &
(BB& '
nameCC 
:CC 
$strCC  
)CC  !
;CC! "
}DD 	
}EE 
}FF �
qD:\Git\NET29ENE25\sesion13\docentes\src\Docentes\Docentes.Infrastructure\Repositories\CursoImpartidoRepository.cs
	namespace 	
Docentes
 
. 
Infrastructure !
.! "
Repositories" .
;. /
internal 
sealed	 
class $
CursoImpartidoRepository .
:/ 0

Repository1 ;
<; <
CursoImpartido< J
>J K
,K L%
ICursoImpartidoRepositoryM f
{ 
public 
$
CursoImpartidoRepository #
(# $ 
ApplicationDbContext$ 8
	dbContext9 B
)B C
:D E
baseF J
(J K
	dbContextK T
)T U
{ 
}		 
}

 �
jD:\Git\NET29ENE25\sesion13\docentes\src\Docentes\Docentes.Infrastructure\Repositories\DocenteRepository.cs
	namespace 	
Docentes
 
. 
Infrastructure !
.! "
Repositories" .
;. /
internal 
sealed	 
class 
DocenteRepository '
:( )

Repository* 4
<4 5
Docente5 <
>< =
,= >
IDocenteRepository? Q
{ 
public 

DocenteRepository 
(  
ApplicationDbContext 1
	dbContext2 ;
); <
:= >
base? C
(C D
	dbContextD M
)M N
{		 
}

 
public 

async 
Task 
< 
Docente 
? 
> 
GetByIdUsuarioAsync  3
(3 4
Guid4 8
	idUsuario9 B
,B C
CancellationTokenD U
cancellationTokenV g
=h i
defaultj q
)q r
{ 
return 
await 
	DbContext 
. 
Set !
<! "
Docente" )
>) *
(* +
)+ ,
., -
FirstOrDefaultAsync- @
(@ A
docente 
=> 
docente 
. 
	UsuarioId (
==) +
	idUsuario, 5
,5 6
cancellationToken7 H
) 	
;	 

} 
} �
cD:\Git\NET29ENE25\sesion13\docentes\src\Docentes\Docentes.Infrastructure\Repositories\Repository.cs
	namespace 	
Docentes
 
. 
Infrastructure !
.! "
Repositories" .
;. /
internal 
abstract	 
class 

Repository "
<" #
T# $
>$ %
where 
T 
: 	
Entity
 
{ 
	protected		 
readonly		  
ApplicationDbContext		 +
	DbContext		, 5
;		5 6
	protected 

Repository 
(  
ApplicationDbContext -
	dbContext. 7
)7 8
{ 
	DbContext 
= 
	dbContext 
; 
} 
public 

async 
Task 
< 
T 
? 
> 
GetByIdAsync &
(& '
Guid 
id 
, 
CancellationToken 
cancellationToken +
=, -
default. 5
) 
{ 
return 
await 
	DbContext 
. 
Set "
<" #
T# $
>$ %
(% &
)& '
. 	
FirstOrDefaultAsync	 
( 
entity #
=>$ &
entity' -
.- .
Id. 0
==1 3
id4 6
,6 7
cancellationToken8 I
)I J
;J K
} 
public 

void 
Add 
( 
T 
entity 
) 
{ 
	DbContext 
. 
Add 
( 
entity 
) 
; 
} 
} �
aD:\Git\NET29ENE25\sesion13\docentes\src\Docentes\Docentes.Infrastructure\Services\CacheService.cs
	namespace 	
Docentes
 
. 
Infrastructure !
.! "
Services" *
;* +
public 
class 
CacheService 
: 
ICacheService )
{ 
private		 
readonly		 "
IConnectionMultiplexer		 +
_connection		, 7
;		7 8
public 

CacheService 
( "
IConnectionMultiplexer .

connection/ 9
)9 :
{ 
_connection 
= 

connection  
;  !
} 
public 

async 
Task 
< 
T 
? 
> 
GetCacheValueAsync ,
<, -
T- .
>. /
(/ 0
string0 6
key7 :
): ;
{ 
var 

db 
= 
_connection 
. 
GetDatabase '
(' (
)( )
;) *
var 

value 
= 
await 
db 
. 
StringGetAsync *
(* +
key+ .
). /
;/ 0
if 	
(
 
value 
. 
IsNullOrEmpty 
) 
{ 
return 
default 
; 
} 
return 
JsonSerializer 
. 
Deserialize (
<( )
T) *
>* +
(+ ,
value, 1
!1 2
)2 3
;3 4
} 
public 

async 
Task 
SetCacheValueAsync (
<( )
T) *
>* +
(+ ,
string, 2
key3 6
,6 7
T8 9
value: ?
,? @
TimeSpanA I
?I J
expirationTimeK Y
=Z [
null\ `
)` a
{ 
var 
db 
= 
_connection 
. 
GetDatabase (
(( )
)) *
;* +
await 
db 
. 
StringSetAsync 
(  
key  #
,# $
JsonSerializer% 3
.3 4
	Serialize4 =
(= >
value> C
)C D
,D E
expirationTimeF T
)T U
;U V
} 
}   �
bD:\Git\NET29ENE25\sesion13\docentes\src\Docentes\Docentes.Infrastructure\Services\CursosService.cs
	namespace 	
Docentes
 
. 
Infrastructure !
.! "
Services" *
;* +
public 
class 
CursosService 
: 
ICursosService +
{ 
private 
readonly 

HttpClient 
_httpClient  +
;+ ,
public		 

CursosService		 
(		 

HttpClient		 #

httpClient		$ .
)		. /
{

 
_httpClient 
= 

httpClient  
;  !
} 
public 

async 
Task 
< 
bool 
> 
CursoExisteAsync ,
(, -
Guid- 1
cursoId2 9
,9 :
CancellationToken; L
cancellationTokenM ^
)^ _
{ 
var 
response 
= 
await 
_httpClient (
.( )
GetAsync) 1
(1 2
$"2 4
$str4 ;
{; <
cursoId< C
}C D
"D E
,E F
cancellationTokenG X
)X Y
;Y Z
return 
response 
. 
IsSuccessStatusCode +
;+ ,
} 
} �
cD:\Git\NET29ENE25\sesion13\docentes\src\Docentes\Docentes.Infrastructure\Services\UsuarioService.cs
	namespace 	
Docentes
 
. 
Infrastructure !
.! "
Services" *
;* +
public 
class 
UsuarioService 
: 
IUsuarioService -
{ 
private 
readonly 

HttpClient 
_httpClient  +
;+ ,
public		 

UsuarioService		 
(		 

HttpClient		 $

httpClient		% /
)		/ 0
{

 
_httpClient 
= 

httpClient  
;  !
} 
public 

async 
Task 
< 
bool 
> 
UsuarioExisteAsync .
(. /
Guid/ 3
	usuarioId4 =
,= >
CancellationToken? P
cancellationTokenQ b
)b c
{ 
var 
response 
= 
await 
_httpClient (
.( )
GetAsync) 1
(1 2
$"2 4
$str4 =
{= >
	usuarioId> G
}G H
"H I
,I J
cancellationTokenK \
)\ ]
;] ^
return 
response 
. 
IsSuccessStatusCode +
;+ ,
} 
} 