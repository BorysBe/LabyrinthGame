// 3dworldVisualStudio.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <stdlib.h>
#include <glut.h>
#include <math.h>
#include <stdio.h>
#include <GL/gl.h>
#include <GL/glu.h>
#include "GridFactory.h"

//#define FPS_counter
#ifdef FPS_counter
//#include "HiResTimer.h"
#endif


char land[] = "land.bmp";
char environment[] = "environment.bmp";
char sky[] = "sky.bmp";
char water[] = "water.bmp";

//****global section - very, very bad... but I didin't had time enough *******
const float    g_DEFAULT_SPEED = 0.4f;
const float     g_WORLD_SIZE = 512;
GLfloat          g_TextOffset = 0.0f;
GLUnurbsObj* g_myNurb;
GLfloat          g_knots[8] = { 0.0, 0.0, 0.0, 0.0, 1.0, 1.0, 1.0, 1.0 };
GLfloat          g_nurb[4][4][3];
GLfloat          g_angle;
GLfloat          g_CameraEye = 5.0f;
GLdouble         g_playerPos[] = { 0.0f, g_CameraEye, 10.0f };
GLdouble         g_objectPos[] = { 0.0f, 6.0f, 0.0f };
GLdouble         g_lookAt[] = { 0.0, 0.0, 0.0 };
GLfloat          g_viewAngle = -90.0;
GLfloat          g_elevationAngle = 0.0;
GLfloat          g_pos[4] = { 5.0, 5.0, 5.0, 0.0 };
GLfloat          g_rotation = 0.0;
GLfloat          g_TerrainSize = 100;
GLuint           g_TerrainMode = 1;
bool             g_WaterDrawing = false;
bool             g_OnlySuperWaterMode = false;
bool             g_MirrorMode = false;
GLfloat          g_WaterLevel = 2;
GLboolean        g_keys[256];
GLuint           g_landTexture;
GLuint           g_environmentTexture;
GLuint           g_skyTexture;
GLuint           g_waterTexture;
#define MAP_X 3 
#define MAP_Z 2
#define MAP_SCALE 20
//auto gridFactory = new grid::GridFactory();
//float ** g_terrain = gridFactory->CreateRandomTerrain();
//float   g_terrain[MAP_X * MAP_Z][3];
//GLuint    g_indexArray[MAP_X * MAP_Z * 2];
int* g_indexArray;
GLfloat   g_colorArray[MAP_X * MAP_Z][3];
GLfloat   g_texcoordArray[MAP_X * MAP_Z][2];

#ifdef FPS_counter
CHiResTimer      g_FPStimer;
#endif
//*******************************************************************************        


//***************bitmap functions - due to problems - not in class - sorry*******
typedef struct tagBITMAPINFOHEADER_1
{
    unsigned int	biSize;
    unsigned long	biWidth;
    unsigned long	biHeight;
    unsigned short	biPlanes;
    unsigned short	biBitCount;
    unsigned int	biCompression;
    unsigned int	biSizeImage;
    unsigned long	biXPelsPerMeter;
    unsigned long	biYPelsPerMeter;
    unsigned int	biClrUsed;
    unsigned int	biClrImportant;
} BITMAPINFOHEADER_1;

#pragma pack(push,2)
typedef struct tagBITMAPFILEHEADER_1
{
    unsigned short	bfType;
    unsigned int	bfSize;
    unsigned short	bfReserved1;
    unsigned short	bfReserved2;
    unsigned int	bfOffBits;
} BITMAPFILEHEADER_1;
#pragma pack(pop)
#define BITMAP_ID   0x4D42 

unsigned char* LoadBitmapFile(char* filename, BITMAPINFOHEADER_1* bitmapInfoHeader)
{
    FILE* filePtr;                        // the file pointer
    BITMAPFILEHEADER_1  bitmapFileHeader;   // bitmap file header
    unsigned char* bitmapImage;        // bitmap image data_s
    unsigned int      imageIdx = 0;       // image index counter
    unsigned char     tempRGB;            // swap variable
    errno_t err;
    FILE* filepoint;
	
    if ((err = fopen_s(&filepoint, filename, "rb")) != 0)
        return NULL;
    fread(&bitmapFileHeader, sizeof(BITMAPFILEHEADER_1), 1, filepoint);  // read the bitmap file header
    if (bitmapFileHeader.bfType != BITMAP_ID)   // verify that this is a bitmap by checking for the universal bitmap id
    {
        fclose(filepoint);
        return NULL;
    }
    fread(bitmapInfoHeader, sizeof(BITMAPINFOHEADER_1), 1, filepoint);  // read the bitmap information header
    fseek(filepoint, bitmapFileHeader.bfOffBits, SEEK_SET);  // move file pointer to beginning of bitmap data
    bitmapImage = (unsigned char*)malloc(bitmapInfoHeader->biSizeImage);  // allocate enough memory for the bitmap image data
    if (!bitmapImage)  // verify memory allocation
    {
        free(bitmapImage);
        fclose(filepoint);
        return NULL;
    }
    fread(bitmapImage, 1, bitmapInfoHeader->biSizeImage, filepoint);  // read in the bitmap image data
    if (bitmapImage == NULL)  // make sure bitmap image data was read
    {
        fclose(filepoint);
        return NULL;
    }

    for (imageIdx = 0; imageIdx < bitmapInfoHeader->biSizeImage; imageIdx += 3)  // swap the R and B values to get RGB since the bitmap color format is in BGR
    {
        tempRGB = bitmapImage[imageIdx];
        bitmapImage[imageIdx] = bitmapImage[imageIdx + 2];
        bitmapImage[imageIdx + 2] = tempRGB;
    }
    fclose(filepoint);  // close the file and return the bitmap image data
    return bitmapImage;
}

void LoadTexture(char* filename, GLuint& texture)
{
    glGenTextures(1, &texture);
    glBindTexture(GL_TEXTURE_2D, texture);
    BITMAPINFOHEADER_1 bitmapInfoHeader;
    unsigned char* v_BUFFER = LoadBitmapFile(filename, &bitmapInfoHeader);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR_MIPMAP_NEAREST);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_NEAREST);
    gluBuild2DMipmaps(GL_TEXTURE_2D, GL_RGB, bitmapInfoHeader.biWidth, bitmapInfoHeader.biHeight, GL_RGB, GL_UNSIGNED_BYTE, v_BUFFER);
    free(v_BUFFER);
}
//****end of Bitmap functions - everything should be in some class in some another file

//*******************settings class *************************************************************
class SettingsClass
{

   

private:

	float * g_terrain;
	static void LoadAllTextures();
	static void SetVariousStartFunctions();
 
    void InitializeVertexTerrain()
    {   
        int z;
        int index = 0;
        int currentVertex;
        auto gridFactory = new grid_factory::GridFactory();

        auto mesh = gridFactory->CreateFlatTerrain(MAP_X,MAP_Z,MAP_SCALE);
        g_terrain = mesh.getVerticesPointer();
        g_indexArray = mesh.getIndicesPointer();
        for (z = 0; z < MAP_Z; z++)
        {
            for (int x = 0; x < MAP_X; x++)
            {
                currentVertex = z * MAP_X + x;
                g_colorArray[currentVertex][0] = g_colorArray[currentVertex][1] = g_colorArray[currentVertex][2] = g_terrain[x + z * MAP_X+1] / 20.0f + 0.5f;      // okresla kolor 
                g_texcoordArray[currentVertex][0] = (float)x;      // okresla wspolrzedne tekstury
                g_texcoordArray[currentVertex][1] = (float)z;
            }
        }

        glEnableClientState(GL_VERTEX_ARRAY);
        glEnableClientState(GL_COLOR_ARRAY);
        glEnableClientState(GL_TEXTURE_COORD_ARRAY);

    	glVertexPointer(3, GL_FLOAT, 0, g_terrain);
        glColorPointer(3, GL_FLOAT, 0, g_colorArray);
        glTexCoordPointer(2, GL_FLOAT, 0, g_texcoordArray);
}
#ifdef FPS_counter
    void InitializeFPSTimer();
#endif
	void InitializeNURBS();
	void CleanUpNURBS();
public:
	void InitializeScene();

};




void SettingsClass::LoadAllTextures()
{
    LoadTexture(land, g_landTexture);
    LoadTexture(environment, g_environmentTexture);
    LoadTexture(sky, g_skyTexture);
    LoadTexture(water, g_waterTexture);
}

void SettingsClass::SetVariousStartFunctions()
{
    glEnable(GL_DEPTH_TEST);
    glEnable(GL_LIGHTING);
    glEnable(GL_LIGHT0);
    glEnable(GL_COLOR_MATERIAL);
    glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
}

#ifdef FPS_counter
void SettingsClass::InitializeFPSTimer()
{
    //g_FPStimer.Init();
}
#endif

void SettingsClass::InitializeNURBS()
{
    glEnable(GL_AUTO_NORMAL);
    int u, v;
    for (u = 0; u < 4; u++)
    {
        for (v = 0; v < 4; v++)
        {
            g_nurb[u][v][0] = 10.0 * ((float)u - 1.5);
            g_nurb[u][v][1] = 6.0 * ((float)v - 1.5);
            if ((u == 1 || u == 2) && (v == 1 || v == 2))
                g_nurb[u][v][2] = 10.0;
            else
                g_nurb[u][v][2] = -6.0;
        }
    }
    g_myNurb = gluNewNurbsRenderer();    // creating NURBS
    gluNurbsProperty(g_myNurb, GLU_SAMPLING_TOLERANCE, 20.0);
    gluNurbsProperty(g_myNurb, GLU_DISPLAY_MODE, GL_LINE);
}

void SettingsClass::CleanUpNURBS()
{
    gluDeleteNurbsRenderer(g_myNurb);
}

void SettingsClass::InitializeScene()
{
#ifdef FPS_counter
    InitializeFPSTimer();
#endif
    SetVariousStartFunctions();
    LoadAllTextures();
    InitializeNURBS();
    InitializeVertexTerrain();
}
//****end of SettingsClass - this class should be in some another file

//**********player class**********************************************
class PlayerClass
{
public:
    void CheckKeyboard();
    static void CatchPlayerInGameWorld();
    void SetCamera();
    static void SetLights();
    static void SetLightPosition1();
    static void SetLightPosition0();
};

void PlayerClass::SetLightPosition1()
{
    g_pos[0] = 0.0f;
    g_pos[1] = 0.0f;
    g_pos[2] = 0.0f;
    g_pos[3] = 0.0f;
}

void PlayerClass::SetLightPosition0()
{
    g_pos[0] = 5.0f;
    g_pos[1] = 5.0f;
    g_pos[2] = 5.0f;
    g_pos[3] = 0.0f;
}

void PlayerClass::CheckKeyboard()
{
    float vRad = float(3.14159 * g_viewAngle / 180.0f);
    if (g_keys[GLUT_KEY_LEFT] == true)
        g_viewAngle -= 2.0;

    if (g_keys[GLUT_KEY_RIGHT] == true)
        g_viewAngle += 2.0;

    if (g_keys[GLUT_KEY_UP] == true)
    {
        g_playerPos[2] += sin(vRad) * g_DEFAULT_SPEED;
        g_playerPos[0] += cos(vRad) * g_DEFAULT_SPEED;
    }
    if (g_keys[GLUT_KEY_DOWN] == true)
    {
        g_playerPos[2] -= sin(vRad) * g_DEFAULT_SPEED;
        g_playerPos[0] -= cos(vRad) * g_DEFAULT_SPEED;
    }

    if (g_keys[87])//letter W, w
        g_CameraEye += 1.0;

    if (g_keys[83])//letter S, s
        g_CameraEye -= 1.0;

    if (g_keys[65])//letter S, s
        g_elevationAngle += 2.0;
    if (g_keys[90])//letter S, s
        g_elevationAngle -= 2.0;
}

void PlayerClass::CatchPlayerInGameWorld()
{
    // nie pozwala graczowi opuscic swiata gry
    if (g_playerPos[0] < -(g_WORLD_SIZE - 200.0f))
        g_playerPos[0] = -(g_WORLD_SIZE - 200.0f);
    if (g_playerPos[0] > (g_WORLD_SIZE - 200.0f))
        g_playerPos[0] = (g_WORLD_SIZE - 200.0f);
    if (g_playerPos[2] < -(g_WORLD_SIZE - 200.0f))
        g_playerPos[2] = -(g_WORLD_SIZE - 200.0f);
    if (g_playerPos[2] > (g_WORLD_SIZE - 200.0f))
        g_playerPos[2] = (g_WORLD_SIZE - 200.0f);
}

void PlayerClass::SetCamera()
{
    float vRad = float(3.14159 * g_viewAngle / 180.0f);  // player angle in radians
    g_lookAt[0] = float(g_playerPos[0] + 100 * cos(vRad));  // setting matrix using player posit.
    g_lookAt[2] = float(g_playerPos[2] + 100 * sin(vRad));
    vRad = float(3.13149 * g_elevationAngle / 180.0f);
    g_lookAt[1] = float(g_playerPos[1] + 100 * sin(vRad));
    glLoadIdentity();
    g_playerPos[1] = g_CameraEye;
    gluLookAt(
        g_playerPos[0], g_playerPos[1], g_playerPos[2],
        g_lookAt[0], g_lookAt[1], g_lookAt[2],
        0.0, 1.0, 0.0
    );
}

void PlayerClass::SetLights()
{
    glLightfv(GL_LIGHT0, GL_POSITION, g_pos);
}
/****end of PlayerClass****************************************************/

//****************glut functions class********************************
class GlutClass
{
public:
	static void ResizeScene(int width, int height);
    static void ProcessNormalKeys(unsigned char key, int x, int y);
    static void PressKey(int key, int x, int y);
    static void ReleaseKey(int key, int x, int y);
};

void GlutClass::PressKey(int key, int x, int y)
{
    g_keys[key] = true;
}

void GlutClass::ReleaseKey(int key, int x, int y)
{
    g_keys[key] = false;
}

void GlutClass::ProcessNormalKeys(unsigned char key, int x, int y)
{
    if ((key == 'W') || (key == 'w'))
        g_CameraEye += 1.0;
    if ((key == 'S') || (key == 's'))
        g_CameraEye -= 1.0;
    if ((key == 'Z') || (key == 'z'))
        g_elevationAngle += 2.0;
    if ((key == 'A') || (key == 'a'))
        g_elevationAngle -= 2.0;
    if (key == 27) //ESC
        exit(0);
    if (key == '1')
    {
        g_WaterDrawing = !g_WaterDrawing;
        g_OnlySuperWaterMode = false;
    }
    if (key == '2')
    {
        g_MirrorMode = !g_MirrorMode;
        g_WaterDrawing = false;
        g_OnlySuperWaterMode = false;
    }
    if (key == '3')
    {
        g_OnlySuperWaterMode = !g_OnlySuperWaterMode;
        switch (g_OnlySuperWaterMode)
        {
        case true:
            g_MirrorMode = true;
            g_WaterDrawing = false;
            break;
        case false:
            g_MirrorMode = false;
            g_WaterDrawing = false;
            break;
        }
    }
    if (key == '+' || key == '=')
    {
        g_TerrainMode += 1;
        if (g_TerrainMode > 4)
            g_TerrainMode = 0;
    }
} //too long :P


void GlutClass::ResizeScene(int width, int height)
{
    if (height == 0)
    {
        height = 1;
    }
    glViewport(0, 0, width, height);
    glMatrixMode(GL_PROJECTION);
    glLoadIdentity();
    gluPerspective(100.0f, (GLfloat)width / (GLfloat)height, 0.1f, 2500.0f);
    glMatrixMode(GL_MODELVIEW);
}
//****end of GlutClass - this class should be in some another file

//*********Drawing class***************************************************************
class DrawingClass
{
private:
	static void Begin2D();
	static void End2D();
	static void EnableMirrorTexture();
	static void WaterAnimation();
	static void TextureMatrixReset();
	static void DisableMirrorTexture();
public:
    PlayerClass* Player1;
    DrawingClass();
	static void EnableBlendMode();
	static void DisableBlendMode();
	static void DrawEnvironment();
	static void DrawFPS();
	static void DrawGround1(float iTerrainSize);
    static void DrawVertexGround();
	static void DrawMirrorObjects();
	static void DrawNormalObjects();
	static void DrawNURBS();
    void DrawOrders();
	static void DrawWater();
	static void DrawWater2();
	static void DrawTerrainBezier();
	static void DrawMyText(char* iString);
	static void DrawRotation();
};

DrawingClass::DrawingClass()
{
    Player1 = new PlayerClass();
    
}

void DrawingClass::DrawVertexGround()
{
    glBindTexture(GL_TEXTURE_2D, g_landTexture);
    glEnable(GL_TEXTURE_2D);
    glTexEnvf(GL_TEXTURE_ENV, GL_TEXTURE_ENV_MODE, GL_MODULATE);
    for (int z = 0; z < MAP_Z - 1; z++)
    {
        glDrawElements(GL_TRIANGLE_STRIP, MAP_X * 2, GL_UNSIGNED_INT, &g_indexArray[z * MAP_X * 2]);
    }
    glDisable(GL_TEXTURE_2D);
}

void DrawingClass::Begin2D()
{
    int width = glutGet(GLUT_WINDOW_WIDTH);
    int height = glutGet(GLUT_WINDOW_HEIGHT);
    glMatrixMode(GL_PROJECTION);
    glPushMatrix();
    glLoadIdentity();
    glOrtho(0, width, 0, height, -1.0f, 1.0f);
    glMatrixMode(GL_MODELVIEW);
    glLoadIdentity();
}

void DrawingClass::End2D()
{
    glMatrixMode(GL_PROJECTION);
    glPopMatrix();
    glMatrixMode(GL_MODELVIEW);
}

void DrawingClass::EnableMirrorTexture()
{
    glTexGenf(GL_S, GL_TEXTURE_GEN_MODE, GL_SPHERE_MAP);
    glTexGenf(GL_T, GL_TEXTURE_GEN_MODE, GL_SPHERE_MAP);
    glEnable(GL_TEXTURE_GEN_S);
    glEnable(GL_TEXTURE_GEN_T);
}

void DrawingClass::EnableBlendMode()
{
    if (!g_MirrorMode)
        return;
    if (g_WaterDrawing)
        return;
    glEnable(GL_BLEND);
    glDepthMask(GL_FALSE);
    glBlendFunc(GL_SRC_ALPHA, GL_ONE);
    glColor4f(1.0f, 1.0f, 1.0f, 0.7f);			// kolor niebieski, przezroczysty
}

void DrawingClass::DisableBlendMode()
{
    glDisable(GL_BLEND);
    glDepthMask(GL_TRUE);
    glBlendFunc(GL_SRC_ALPHA, GL_ONE);
    glColor4f(1.0f, 1.0f, 1.0f, 0.7f);			// kolor niebieski, przezroczysty
}

void DrawingClass::WaterAnimation()
{
    glMatrixMode(GL_TEXTURE);
    glLoadIdentity();
    glTranslatef(g_TextOffset, 0.0f, 0.0f);
    glMatrixMode(GL_MODELVIEW);
    g_TextOffset += 0.02;
}

void DrawingClass::TextureMatrixReset()
{
    glMatrixMode(GL_TEXTURE);
    glLoadIdentity();
    glMatrixMode(GL_MODELVIEW);
}

void DrawingClass::DisableMirrorTexture()
{
    glTexGenf(GL_S, GL_TEXTURE_GEN_MODE, GL_SPHERE_MAP);
    glTexGenf(GL_T, GL_TEXTURE_GEN_MODE, GL_SPHERE_MAP);
    glDisable(GL_TEXTURE_GEN_S);
    glDisable(GL_TEXTURE_GEN_T);
}

void DrawingClass::DrawMyText(char* iString)
{
    char* p;
    for (p = iString; *p; p++)
        glutBitmapCharacter(GLUT_BITMAP_TIMES_ROMAN_24, *p);
}

void DrawingClass::DrawNormalObjects()
{
    glPushAttrib(GL_CURRENT_BIT);
    glPushMatrix();
    g_objectPos[1] += 0.001;
    g_objectPos[2] += 0.01;
    glTranslatef(g_objectPos[0], g_objectPos[1], g_objectPos[2]);
    glBindTexture(GL_TEXTURE_2D, g_landTexture);
    EnableMirrorTexture();
    glEnable(GL_TEXTURE_2D);
    DisableBlendMode();
    glutSolidTeapot(3);
    if (g_MirrorMode)
        EnableBlendMode();
    glDisable(GL_TEXTURE_2D);
    DisableMirrorTexture();
    glPopMatrix();
    glPopAttrib();
} // DrawNormalObjects()

void DrawingClass::DrawEnvironment()
{
    glEnable(GL_TEXTURE_2D);
    glTexEnvf(GL_TEXTURE_2D, GL_TEXTURE_ENV_MODE, GL_REPLACE);
    glBindTexture(GL_TEXTURE_2D, g_environmentTexture);

    glBegin(GL_QUADS);
    // plaszczyzna -z
    glTexCoord2i(0, 0); glVertex3i(-g_WORLD_SIZE, 0, -g_WORLD_SIZE);
    glTexCoord2i(0, 2); glVertex3i(g_WORLD_SIZE, 0, -g_WORLD_SIZE);
    glTexCoord2i(1, 2); glVertex3i(g_WORLD_SIZE, g_WORLD_SIZE, -g_WORLD_SIZE);
    glTexCoord2i(1, 0); glVertex3i(-g_WORLD_SIZE, g_WORLD_SIZE, -g_WORLD_SIZE);
    // plaszczyzna x 
    glTexCoord2i(0, 0); glVertex3i(g_WORLD_SIZE, 0, -g_WORLD_SIZE);
    glTexCoord2i(0, 2); glVertex3i(g_WORLD_SIZE, 0, g_WORLD_SIZE);
    glTexCoord2i(1, 2); glVertex3i(g_WORLD_SIZE, g_WORLD_SIZE, g_WORLD_SIZE);
    glTexCoord2i(1, 0); glVertex3i(g_WORLD_SIZE, g_WORLD_SIZE, -g_WORLD_SIZE);
    // plaszczyzna z 
    glTexCoord2i(0, 0); glVertex3i(g_WORLD_SIZE, 0, g_WORLD_SIZE);
    glTexCoord2i(0, 2); glVertex3i(-g_WORLD_SIZE, 0, g_WORLD_SIZE);
    glTexCoord2i(1, 2); glVertex3i(-g_WORLD_SIZE, g_WORLD_SIZE, g_WORLD_SIZE);
    glTexCoord2i(1, 0); glVertex3i(g_WORLD_SIZE, g_WORLD_SIZE, g_WORLD_SIZE);
    // plaszczyzna -x 
    glTexCoord2i(0, 0); glVertex3i(-g_WORLD_SIZE, 0, g_WORLD_SIZE);
    glTexCoord2i(0, 2); glVertex3i(-g_WORLD_SIZE, 0, -g_WORLD_SIZE);
    glTexCoord2i(1, 2); glVertex3i(-g_WORLD_SIZE, g_WORLD_SIZE, -g_WORLD_SIZE);
    glTexCoord2i(1, 0); glVertex3i(-g_WORLD_SIZE, g_WORLD_SIZE, g_WORLD_SIZE);
    glEnd();
    glBindTexture(GL_TEXTURE_2D, g_skyTexture);
    glBegin(GL_QUADS);
    glTexCoord2i(0, 0); glVertex3i(-g_WORLD_SIZE, g_WORLD_SIZE - 1.0, -g_WORLD_SIZE);
    glTexCoord2i(0, 1); glVertex3i(g_WORLD_SIZE, g_WORLD_SIZE - 1.0, -g_WORLD_SIZE);
    glTexCoord2i(1, 1); glVertex3i(g_WORLD_SIZE, g_WORLD_SIZE - 1.0, g_WORLD_SIZE);
    glTexCoord2i(1, 0); glVertex3i(-g_WORLD_SIZE, g_WORLD_SIZE - 1.0, g_WORLD_SIZE);
    glEnd();
    glDisable(GL_TEXTURE_2D);
}

void DrawingClass::DrawGround1(float iTerrainSize)
{
    int p;
    p = iTerrainSize;
    glPushMatrix();
    glEnable(GL_TEXTURE_2D);
    glBindTexture(GL_TEXTURE_2D, g_landTexture);
    glBegin(GL_QUADS);
    glTexCoord2i(0, 0);             glVertex3i(-1 * p, 2, -1 * p);
    glTexCoord2i(0, 1 * (p / 2));       glVertex3i(1 * p, 2, -1 * p);
    glTexCoord2i(1 * (p / 2), 1 * (p / 2)); glVertex3i(1 * p, 2, 1 * p);
    glTexCoord2i(1 * (p / 2), 0);       glVertex3i(-1 * p, 2, 1 * p);
    glEnd();
    glDisable(GL_TEXTURE_2D);
    glPopMatrix();
}

void DrawingClass::DrawWater2()
{
    int p;
    p = g_WORLD_SIZE;
    glPushMatrix();
    WaterAnimation();
    glEnable(GL_TEXTURE_2D);
    glBindTexture(GL_TEXTURE_2D, g_waterTexture);
    glBegin(GL_QUADS);
    glTexCoord2i(0, 0);             glVertex3i(-1 * p, g_WaterLevel - 5, -1 * p);
    glTexCoord2i(0, 1 * (p / 2));       glVertex3i(1 * p, g_WaterLevel - 5, -1 * p);
    glTexCoord2i(1 * (p / 2), 1 * (p / 2)); glVertex3i(1 * p, g_WaterLevel - 5, 1 * p);
    glTexCoord2i(1 * (p / 2), 0);       glVertex3i(-1 * p, g_WaterLevel - 5, 1 * p);
    glEnd();
    glBegin(GL_QUADS);
    glTexCoord2i(0, 0);             glVertex3i(-1 * p, g_WaterLevel + 0, -1 * p);
    glTexCoord2i(0, 1 * (p / 2));       glVertex3i(1 * p, g_WaterLevel + 0, -1 * p);
    glTexCoord2i(1 * (p / 2), 1 * (p / 2)); glVertex3i(1 * p, g_WaterLevel + 0, 1 * p);
    glTexCoord2i(1 * (p / 2), 0);       glVertex3i(-1 * p, g_WaterLevel + 0, 1 * p);
    glEnd();
    TextureMatrixReset();
    glDisable(GL_TEXTURE_2D);
    glPopMatrix();
}

void DrawingClass::DrawTerrainBezier()
{
    float cSurface[3][3][3] = { { { -200.0, 40.0, 200.0 }, { -100.0, 100.0, 200.0 }, { 200.0, 0.0, 200.0 } },
                                { { -240.0, 0.0, 0.0 }, { -150.0, 100.0, 0.0 },	{ 200.0, 0.0, 0.0 } },
                                { { -200.0, -80.0, -200.0 }, { -100.0, 100.0, -200.0 },	{ 200.0, 0.0, -200.0 } } };
    float sTexCoords[2][2][2] = { {{0.0, 0.0}, {0.0, 50.0}}, {{50.0, 0.0}, {50.0, 50.0}} };

    if (g_WaterDrawing)
    {
        WaterAnimation();
        glEnable(GL_BLEND);
        glDepthMask(GL_FALSE);
        glBlendFunc(GL_SRC_ALPHA, GL_ONE);
        glColor4f(1.0f, 1.0f, 1.0f, 0.7f);
        glBindTexture(GL_TEXTURE_2D, g_waterTexture);
    }
    else
        glBindTexture(GL_TEXTURE_2D, g_landTexture);

    glEnable(GL_TEXTURE_2D);
    glMap2f(GL_MAP2_VERTEX_3, 0.0, 1.0, 3, 3, 0.0, 1.0, 9, 3, &cSurface[0][0][0]);
    glMap2f(GL_MAP2_TEXTURE_COORD_2, 0, 1, 2, 2, 0, 1, 4, 2, &sTexCoords[0][0][0]); // tworzy ewaluator wspolrzednych tekstury
    glEnable(GL_MAP2_TEXTURE_COORD_2);     // aktywuje utworzone ewaluatory
    glEnable(GL_MAP2_VERTEX_3);
    glMapGrid2f(10, 0.0f, 1.0f, 10, 0.0f, 1.0f);     // tworzy siatke powierzchni
    glEnable(GL_AUTO_NORMAL);
    glEvalMesh2(GL_FILL, 0, 10, 0, 10);
    glDisable(GL_TEXTURE_2D);
    glPointSize(10.0);
    glColor3f(1.0, 1.0, 0.0);
    glBegin(GL_POINTS);
    for (int i = 0; i < 3; i++)
        for (int j = 0; j < 3; j++)
            glVertex3fv(&cSurface[i][j][0]);
    glEnd();
    glPointSize(1.0);
    glColor3f(1.0, 1.0, 1.0);
    DisableBlendMode();
    TextureMatrixReset();
    glMatrixMode(GL_MODELVIEW);
}

void DrawingClass::DrawNURBS()
{
    glPushMatrix();
    glEnable(GL_TEXTURE_2D);
    glTranslatef(0.0, 0.0, -10.0);
    glRotatef(290.0, 1.0, 0.0, 0.0);
    g_angle += 2.0;
    glPushMatrix();
    glRotatef(g_angle, 0.0, 0.0, 1.0);		// obraca powierzchnie
    gluBeginSurface(g_myNurb); 		// rozpoczyna definiowanie powierzchni B-sklejanej
    gluNurbsSurface(g_myNurb, 8, g_knots, 8, g_knots, 4 * 3, 3, &g_nurb[0][0][0], 4, 4, GL_MAP2_VERTEX_3);     	// rysuje powierzchnie B-sklejana
    gluEndSurface(g_myNurb); 		// rysuje punkty kontrolne
    glPointSize(6.0);
    glColor3f(1.0, 1.0, 0.0);
    glDisable(GL_TEXTURE_2D);
    glBegin(GL_POINTS);
    for (int i = 0; i < 4; i++)
        for (int j = 0; j < 4; j++)
            glVertex3fv(&g_nurb[i][j][0]);
    glEnd();
    glPointSize(1.0);
    glPopMatrix();
    if (g_angle == 360)
        g_angle = 0;
    glColor3f(1.0, 1.0, 1.0);
    glDisable(GL_TEXTURE_2D);
    glPopMatrix();
}

void DrawingClass::DrawMirrorObjects()
{
    glPushMatrix();
    glScalef(1.0, -1.5, 1.0);
    glLightfv(GL_LIGHT0, GL_POSITION, g_pos);
    DrawingClass::DrawNormalObjects();
    DrawingClass::DrawEnvironment();
    glPopMatrix();
}

void DrawingClass::DrawRotation()
{
    g_rotation += 1.0;
    if (g_rotation > 360.0)
        g_rotation = 0.0;
    DrawNormalObjects();
}


void DrawingClass::DrawWater()
{
    int p;
    p = g_WORLD_SIZE * 2;

    glEnable(GL_TEXTURE_2D);
    glEnable(GL_BLEND);
    glDepthMask(GL_FALSE);
    glBlendFunc(GL_SRC_ALPHA, GL_ONE);
    glColor4f(1.0f, 1.0f, 1.0f, 0.7f);			// kolor niebieski, przezroczysty
    glBindTexture(GL_TEXTURE_2D, g_waterTexture);		// wybiera teksture wody
    glTexEnvf(GL_TEXTURE_ENV, GL_TEXTURE_ENV_MODE, GL_MODULATE);
    glPushMatrix();
    WaterAnimation();
    glBegin(GL_QUADS);
    glTexCoord2i(0, 0);             glVertex3i(-1 * p, g_WaterLevel - 5, -1 * p);
    glTexCoord2i(0, 1 * (p / 2));       glVertex3i(1 * p, g_WaterLevel - 5, -1 * p);
    glTexCoord2i(1 * (p / 2), 1 * (p / 2)); glVertex3i(1 * p, g_WaterLevel - 5, 1 * p);
    glTexCoord2i(1 * (p / 2), 0);       glVertex3i(-1 * p, g_WaterLevel - 5, 1 * p);
    glEnd();
    glBegin(GL_QUADS);
    glTexCoord2i(0, 0);             glVertex3i(-1 * p, g_WaterLevel + 0, -1 * p);
    glTexCoord2i(0, 1 * (p / 2));       glVertex3i(1 * p, g_WaterLevel + 0, -1 * p);
    glTexCoord2i(1 * (p / 2), 1 * (p / 2)); glVertex3i(1 * p, g_WaterLevel + 0, 1 * p);
    glTexCoord2i(1 * (p / 2), 0);       glVertex3i(-1 * p, g_WaterLevel + 0, 1 * p);
    glEnd();
    glPopMatrix();
    glDisable(GL_TEXTURE_2D);
    glColor4f(1.0f, 1.0f, 1.0f, 0.0f);
    glDepthMask(GL_TRUE);
    glDisable(GL_BLEND);
    TextureMatrixReset();
}

void DrawingClass::DrawFPS()
{
    Begin2D();
#ifdef FPS_counter
    char cOutBuffer[32];
    // wyswietla tworzenia klatek animacji
    glColor4f(1.0f, 1.0f, 1.0f, 1.0f);
    glRasterPos2f(0.0f, glutGet(GLUT_WINDOW_HEIGHT) - 30);
    sprintf(cOutBuffer, "FPS: %0.1f", g_FPStimer.GetFPS());
    DrawingClass::DrawMyText(cOutBuffer);
#endif
    End2D();
}

void DrawingClass::DrawOrders()
{
    Begin2D();
    char cOutBuffer[128];
    glColor4f(1.0f, 1.0f, 1.0f, 1.0f);
    Player1->SetLightPosition0();
    Player1->SetLights();
    glRasterPos2f(0.0f, glutGet(GLUT_WINDOW_HEIGHT) - 60);
    sprintf_s(cOutBuffer, " ... ");
    switch (g_TerrainMode)
    {
    case 1: sprintf_s(cOutBuffer, "Nacisnij '+' by zobaczyc powierzchnie stworzona z siatki wysokosci");
        break;
    case 2: sprintf_s(cOutBuffer, "Nacisnij '+' by zobaczyc powierzchnie Beziera");
        break;
    case 3: sprintf_s(cOutBuffer, "Nacisnij '+' by zobaczyc powierzchnie B-sklejana (NURBS)");
        break;
    case 0: sprintf_s(cOutBuffer, "Nacisnij '+' by zobaczyc plaszczyzne");
        break;
    }
    DrawMyText(cOutBuffer);
    glRasterPos2f(0.0f, glutGet(GLUT_WINDOW_HEIGHT) - 90);
    sprintf_s(cOutBuffer, "Klawiszami Przod, Tyl, Lewo, Prawo - poeruszasz sie po planszy");
    DrawMyText(cOutBuffer);
    glRasterPos2f(0.0f, glutGet(GLUT_WINDOW_HEIGHT) - 120);
    sprintf_s(cOutBuffer, "Klawiszami A lub Z - patrzysz w gore albo w dol");
    DrawMyText(cOutBuffer);
    glRasterPos2f(0.0f, glutGet(GLUT_WINDOW_HEIGHT) - 150);
    sprintf_s(cOutBuffer, "Klawiszami W lub S - podnosisz kamere w dol/gore");
    DrawMyText(cOutBuffer);
    glRasterPos2f(0.0f, glutGet(GLUT_WINDOW_HEIGHT) - 180);
    sprintf_s(cOutBuffer, "Sprobuj roznych trybow renderowania wody (1, 2, 3) i ich kombinacji dla KAZDEJ z powierzchni");
    DrawMyText(cOutBuffer);
    End2D();
}
//****end of DrawingClass - this class shopuld be in some another file

//********************Display class**********************************************************
class DisplayClass
{
private:
	static void ClearDepthBuffor();
public:
    DrawingClass* Drawer1;
    SettingsClass* Settings1;
    DisplayClass();
    void DisplayScene();
    static void Clean();
};

DisplayClass::DisplayClass()
{
    Drawer1 = new DrawingClass();
    Settings1 = new SettingsClass();
}


void DisplayClass::Clean()
{
    gluDeleteNurbsRenderer(g_myNurb);
}

void DisplayClass::ClearDepthBuffor()
{
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
}

void DisplayClass::DisplayScene()
{
    ClearDepthBuffor();
    Drawer1->Player1->CheckKeyboard();
    Drawer1->Player1->CatchPlayerInGameWorld();
    Drawer1->Player1->SetCamera();
    ClearDepthBuffor();
    Drawer1->Player1->SetLights();
    Drawer1->DrawRotation();
    if (g_WaterDrawing && (!g_OnlySuperWaterMode))
        Drawer1->DrawWater();
    Drawer1->DrawEnvironment();
    Drawer1->EnableBlendMode();
    if (g_OnlySuperWaterMode)
        Drawer1->DrawWater2();

    if (!g_OnlySuperWaterMode)
        switch (g_TerrainMode)
        {
        case 1:
            Drawer1->DrawGround1(g_TerrainSize);
            break;
        case 2:
            Drawer1->DrawVertexGround();
            break;
        case 3:
            Drawer1->DrawTerrainBezier();
            break;
        case 4:
            Drawer1->DrawNURBS();
            break;

        }
    if (!g_WaterDrawing)
        Drawer1->DrawMirrorObjects();

    Drawer1->DisableBlendMode();
    Drawer1->DrawFPS();
    Drawer1->DrawOrders();
    glutSwapBuffers();
}
//***end of display class ***********************************************

void MyGlutKey(unsigned char key, int x, int y)
{
    GlutClass* GlutFunc1;
    GlutFunc1 = new GlutClass();
    GlutFunc1->ProcessNormalKeys(key, x, y);
}

void MyPressKey(int key, int x, int y)
{
    GlutClass* GlutFunc1;
    GlutFunc1 = new GlutClass();
    GlutFunc1->PressKey(key, x, y);
}

void MyReleaseKey(int key, int x, int y)
{
    GlutClass* GlutFunc1;
    GlutFunc1 = new GlutClass();
    GlutFunc1->ReleaseKey(key, x, y);
}

void MyGlutResize(int width, int height)
{
    GlutClass* GlutFunc1;
    GlutFunc1 = new GlutClass();
    GlutFunc1->ResizeScene(width, height);
}

void MyGlutDisplay(void)
{
    DisplayClass* vDisplayer;
    vDisplayer = new DisplayClass();
    vDisplayer->DisplayScene();
}

int main(int argc, char** argv)
{
    DisplayClass vDisplayer;
    glutInit(&argc, argv);
    glutInitDisplayMode(GLUT_DEPTH | GLUT_DOUBLE | GLUT_RGBA);
    glutInitWindowPosition(0, 0);
    glutInitWindowSize(1280, 1024);
    glutCreateWindow("First program");
    vDisplayer.Settings1->InitializeScene();
    glutIgnoreKeyRepeat(0);
    glutKeyboardFunc(MyGlutKey);
    glutDisplayFunc(MyGlutDisplay);
    glutIdleFunc(MyGlutDisplay);
    glutReshapeFunc(MyGlutResize);
    glutSpecialFunc(MyPressKey);
    glutSpecialUpFunc(MyReleaseKey);
    glutMainLoop();
    vDisplayer.Clean();
    return(0);
}


