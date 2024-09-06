@echo off
setlocal enabledelayedexpansion


set "source_folder=C:\Users\Mert\Desktop\Kaynak"
set "target_folder=C:\Users\Mert\Desktop\Hedef"


for /d /r "%target_folder%" %%d in (*) do (
    set "relative_path=%%d"
    set "relative_path=!relative_path:%target_folder%=!"
    set "source_folder_check=%source_folder%!relative_path!"
    
    if not exist "!source_folder_check!" (
       
        echo [INFO] Hedef klasor kaynakta bulunamadi, siliniyor: %%d
        
    )
)


for /r "%source_folder%" %%f in (*) do (
    
    for /f %%a in ('powershell -command "Get-FileHash -Algorithm SHA256 -Path \"%%f\" | Select-Object -ExpandProperty Hash"') do set "source_hash=%%a"
    
   
    set "file_extension=%%~xf"

    
    set "relative_path=%%f"
    set "relative_path=!relative_path:%source_folder%=!"
    set "target_file=%target_folder%!relative_path!"

    
    if not exist "!target_file!\.." mkdir "!target_file!\.."

    
    if exist "!target_file!" (
        for /f %%b in ('powershell -command "Get-FileHash -Algorithm SHA256 -Path \"!target_file!\" | Select-Object -ExpandProperty Hash"') do set "target_hash=%%b"

        
        if /i "!source_hash!" neq "!target_hash!" (
            
            set "date_part=%date:~10,4%.%date:~7,2%.%date:~4,2%"
            set "time_part=%time:~0,2%.%time:~3,2%"
            set "date_part=!date_part: =0!"
            set "time_part=!time_part: =0!"
            set "timestamp=!date_part!_!time_part!"
            set "versioned_file=%target_folder%!relative_path!_!timestamp!!file_extension!"

            
            echo [INFO] Dosyalar degisti: !relative_path!, eski dosya versiyonlanacak.
            copy "!target_file!" "!versioned_file!"
            
            
            copy "%%f" "!target_file!"
        ) else (
            echo [INFO] Dosya ayni: !relative_path!, kopyalamaya gerek yok.
        )
    ) else (
        
        echo [INFO] Dosya hedefte bulunamadi: !relative_path!, kopyalanacak.
        copy "%%f" "!target_file!"
    )
)

endlocal
echo.
echo ================================
echo.
echo Islem Basariyla Tamamlandi.
echo.
echo.
echo ================================
echo 2024 DDO Stajyer Mert Bilger
echo ================================
echo.
pause
