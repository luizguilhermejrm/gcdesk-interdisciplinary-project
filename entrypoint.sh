#!/bin/bash
sed -i "s/\${DB_HOST}/$DB_HOST/g; s/\${DB_USER}/$DB_USER/g; s/\${DB_PASS}/$DB_PASS/g" Web.config
exec xsp4 --port 80 --nonstop
